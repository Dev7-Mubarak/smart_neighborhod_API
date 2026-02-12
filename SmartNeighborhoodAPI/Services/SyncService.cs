using Microsoft.EntityFrameworkCore;
using SmartNeighborhoodAPI.Helpers.DTOs.Sync;
using SmartNeighborhoodAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartNeighborhoodAPI.Services
{

    public class SyncService : ISyncService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SyncService> _logger;

        public SyncService(ApplicationDbContext dbContext, ILogger<SyncService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public async Task<SyncPullResponse> PullChangesAsync(
            DateTime syncToken,
            string? entityTypes = null,
            int batchSize = 1000,
            bool includeDeleted = true,
            string? userId = null)
        {
            var response = new SyncPullResponse
            {
                RequestedAt = DateTime.UtcNow,
                ServerTimestamp = DateTime.UtcNow,
                Changes = new Dictionary<string, List<dynamic>>()
            };

            try
            {
                var typesToSync = GetEntityTypesToSync(entityTypes);

                foreach (var entityType in typesToSync)
                {
                    var changes = await GetChangedEntitiesAsync(
                        entityType,
                        syncToken,
                        batchSize,
                        includeDeleted,
                        userId!
                    );

                    if (changes.Any())
                    {
                        response.Changes[entityType] = changes;
                    }
                }

                response.HasMore = response.Changes.Values
                    .Any(c => c.Count >= batchSize);

                response.NextSyncToken = response.ServerTimestamp;

                _logger.LogInformation(
                    $"Pull sync completed: {response.Changes.Count} entity types, " +
                    $"hasMore={response.HasMore}");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PullChangesAsync: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<SyncPushResponse> PushChangesAsync(
            SyncPushRequest request,
            string userId,
            string? idempotencyKey = null)
        {
            // Start database transaction
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var response = new SyncPushResponse
                    {
                        RequestedAt = request.RequestedAt,
                        ProcessedAt = DateTime.UtcNow,
                        ServerTimestamp = DateTime.UtcNow,
                        Results = new Dictionary<string, SyncResultDetails>()
                    };

                    response.TotalProcessed = response.Results.Values
                        .Sum(r => r.Created.Count + r.Updated.Count + r.Deleted.Count);

                    response.TotalConflicts = response.Results.Values
                        .Sum(r => r.Conflicts.Count);

                    response.SyncStatus = response.TotalConflicts == 0 ? "success" : "partial_success";

                    if (response.SyncStatus == "success")
                    {
                        await transaction.CommitAsync();
                        _logger.LogInformation(
                            $"Push sync successful: processed={response.TotalProcessed}, " +
                            $"conflicts={response.TotalConflicts}");
                    }
                    else
                    {
                        // Still commit but indicate partial success
                        await transaction.CommitAsync();
                        _logger.LogWarning(
                            $"Push sync partial success: processed={response.TotalProcessed}, " +
                            $"conflicts={response.TotalConflicts}");
                    }

                    return response;
                }
                catch (DbUpdateException dbEx)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError($"Database error in PushChangesAsync: {dbEx.Message}", dbEx);
                    throw new InvalidOperationException("Failed to process sync changes", dbEx);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError($"Error in PushChangesAsync: {ex.Message}", ex);
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets sync status without performing actual sync.
        /// </summary>
        public async Task<SyncStatusResponse> GetSyncStatusAsync(string userId)
        {
            try
            {
                return new SyncStatusResponse
                {
                    IsSyncAvailable = true,
                    LastSuccessfulSync = DateTime.UtcNow.AddMinutes(-30), // TODO: Get actual last sync time
                    PendingChangesEstimate = 0, // TODO: Calculate pending changes
                    ServerVersion = "2.0",
                    MinimumClientVersion = "1.5",
                    Recommendations = new List<string>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetSyncStatusAsync: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<List<dynamic>> GetChangedEntitiesAsync(
            string entityType,
            DateTime since,
            int batchSize,
            bool includeDeleted,
            string userId)
        {
            return entityType.ToLower() switch
            {
                "issues" => await QueryIssuesAsync(since, batchSize, includeDeleted, userId),
                "persons" => await QueryPersonsAsync(since, batchSize, includeDeleted, userId),
                "families" => await QueryFamiliesAsync(since, batchSize, includeDeleted, userId),

                _ => new List<dynamic>()
            };
        }

        public dynamic ResolveConflict(object clientVersion, object serverVersion)
        {

            return new
            {
                resolution = "LAST_WRITE_WINS",
                winner = "server",
                clientTimestamp = clientVersion,
                serverTimestamp = serverVersion
            };
        }


        public async Task<dynamic> MapClientIdToServerIdAsync(
            string entityType,
            string clientId,
            Guid serverId)
        {
   

            return new
            {
                clientId = clientId,
                serverId = serverId,
                entityType = entityType,
                mappedAt = DateTime.UtcNow
            };
        }


        private List<string> GetEntityTypesToSync(string? entityTypes)
        {
            if (string.IsNullOrWhiteSpace(entityTypes))
            {
                // Return all sync-enabled entity types
                return new List<string>
                {
                    "issues",
                    "persons",
                    "families",
                    // TODO: Add all other sync-enabled entity types
                    // "familymembers",
                    // "blocks",
                    // "residentialneighborhoods",
                    // "conflictcases",
                    // "projects",
                    // etc.
                };
            }

            var types = entityTypes
                .Split(',')
                .Select(e => e.Trim().ToLower())
                .Where(e => !string.IsNullOrEmpty(e))
                .ToList();

            return types;
        }

        private async Task<List<dynamic>> QueryIssuesAsync(
            DateTime since,
            int batchSize,
            bool includeDeleted,
            string userId)
        {
            try
            {
                IQueryable<dynamic> query = _dbContext.Issues
                    .AsNoTracking()
                    .Where(i => i.UpdatedAt > since);

                // Filter deleted records unless explicitly requested
                if (!includeDeleted)
                {
                    query = query.Where(i => !i.IsDeleted);
                }

                var results = await query
                    .OrderBy(i => i.UpdatedAt)
                    .Take(batchSize)
                    .Select(i => new
                    {
                        i.Id,
                        i.Title,
                        i.Description,
                        i.Category,
                        i.Priority,
                        i.Status,
                        i.ReporterId,
                        i.AssigneeId,
                        i.CreatedAt,
                        i.UpdatedAt,
                        i.IsDeleted,
                        i.DeletedAt,
                        i.ClientId
                    })
                    .Cast<dynamic>()
                    .ToListAsync();

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error querying issues: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// Queries Persons table for changes since specified timestamp.
        /// 
        /// PERFORMANCE CRITICAL: Must use index on (IsDeleted, UpdatedAt)
        /// </summary>
        private async Task<List<dynamic>> QueryPersonsAsync(
            DateTime since,
            int batchSize,
            bool includeDeleted,
            string userId)
        {
            try
            {
                IQueryable<dynamic> query = _dbContext.Persons
                    .AsNoTracking()
                    .Where(p => p.UpdatedAt > since);

                if (!includeDeleted)
                {
                    query = query.Where(p => !p.IsDeleted);
                }

                var results = await query
                    .OrderBy(p => p.UpdatedAt)
                    .Take(batchSize)
                    .Select(p => new
                    {
                        p.Id,
                        p.FirstName,
                        p.SecondName,
                        p.ThirdName,
                        p.LastName,
                        p.PhoneNumber,
                        p.Gender,
                        p.BloodType,
                        p.CreatedAt,
                        p.UpdatedAt,
                        p.IsDeleted,
                        p.DeletedAt,
                        p.ClientId
                    })
                    .Cast<dynamic>()
                    .ToListAsync();

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error querying persons: {ex.Message}", ex);
                throw;
            }
        }

        private async Task<List<dynamic>> QueryFamiliesAsync(
            DateTime since,
            int batchSize,
            bool includeDeleted,
            string userId)
        {
            try
            {
                IQueryable<dynamic> query = _dbContext.Families
                    .AsNoTracking()
                    .Where(f => f.UpdatedAt > since);

                if (!includeDeleted)
                {
                    query = query.Where(f => !f.IsDeleted);
                }

                var results = await query
                    .OrderBy(f => f.UpdatedAt)
                    .Take(batchSize)
                    .Select(f => new
                    {
                        f.Id,
                        f.Name,
                        f.Location,
                        f.FamilyNotes,
                        f.FamilyCategoryId,
                        f.HousingType,
                        f.BlockId,
                        f.CreatedAt,
                        f.UpdatedAt,
                        f.IsDeleted,
                        f.DeletedAt,
                        f.ClientId
                    })
                    .Cast<dynamic>()
                    .ToListAsync();

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error querying families: {ex.Message}", ex);
                throw;
            }
        }


    }
}
