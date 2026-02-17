using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.DTOs.Sync;
using SmartNeighborhoodAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartNeighborhoodAPI.Services
{

    public class SyncService : ISyncService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SyncService> _logger;
        private readonly IMapper _mapper;

        public SyncService(
            ApplicationDbContext dbContext,
            ILogger<SyncService> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
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

        /// <summary>
        /// Master method: Opens a transaction, processes all entity types, and commits.
        /// Implements the "Dual-Generic Processor Strategy" to handle mixed ID types.
        /// </summary>
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

                    // Process entities with INT primary keys
                    // Person (Int)
                    if (request.Changes.Persons != null)
                    {
                        var personResult = await ProcessIntEntitiesAsync<Person, PersonChangeDto>(
                            request.Changes.Persons,
                            "persons",
                            userId);
                        response.Results["persons"] = personResult;
                    }

                    // Family (Int)
                    if (request.Changes.Families != null)
                    {
                        var familyResult = await ProcessIntEntitiesAsync<Family, FamilyChangeDto>(
                            request.Changes.Families,
                            "families",
                            userId);
                        response.Results["families"] = familyResult;
                    }

                    // TODO: Add future int-based entities here
                    // Example: FamilyMember (Int), Block (Int), etc.
                    // if (request.Changes.FamilyMembers != null)
                    // {
                    //     var familyMemberResult = await ProcessIntEntitiesAsync<FamilyMember, FamilyMemberChangeDto>(
                    //         request.Changes.FamilyMembers,
                    //         "familymembers",
                    //         userId);
                    //     response.Results["familymembers"] = familyMemberResult;
                    // }

                    // Process entities with GUID primary keys
                    // Issue (Guid)
                    if (request.Changes.Issues != null)
                    {
                        var issueResult = await ProcessGuidEntitiesAsync<Issue, IssueChangeDto>(
                            request.Changes.Issues,
                            "issues",
                            userId);
                        response.Results["issues"] = issueResult;
                    }

                    // TODO: Add future Guid-based entities here

                    // Calculate summary
                    response.TotalProcessed = response.Results.Values
                        .Sum(r => r.Created.Count + r.Updated.Count + r.Deleted.Count);

                    response.TotalConflicts = response.Results.Values
                        .Sum(r => r.Conflicts.Count);

                    response.SyncStatus = response.TotalConflicts == 0 ? "success" : "partial_success";

                    // Commit transaction
                    await transaction.CommitAsync();

                    _logger.LogInformation(
                        $"Push sync {response.SyncStatus}: processed={response.TotalProcessed}, " +
                        $"conflicts={response.TotalConflicts}");

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
        /// Processor A: Handles entities with INT primary keys (Person, Family, etc.)
        /// - Create: Adds entity, saves immediately to generate ID, returns clientId->serverId mapping
        /// - Update: Parses string Id to int, finds entity, updates with conflict detection
        /// - Delete: Parses string Id to int, finds entity, performs soft delete
        /// </summary>
        private async Task<SyncResultDetails> ProcessIntEntitiesAsync<TEntity, TDto>(
            EntityChanges<TDto> changes,
            string entityTypeName,
            string userId)
            where TEntity : class, ISyncable, new()
            where TDto : class
        {
            var result = new SyncResultDetails();

            try
            {
                // Get the Id property using reflection
                var idProperty = typeof(TEntity).GetProperty("Id");
                if (idProperty == null || idProperty.PropertyType != typeof(int))
                {
                    throw new InvalidOperationException($"Entity {typeof(TEntity).Name} must have an 'int Id' property");
                }

                // Process CREATE operations
                foreach (var createDto in changes.Created)
                {
                    try
                    {
                        var entity = _mapper.Map<TEntity>(createDto);

                        // Ensure sync fields are set
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.UpdatedAt = DateTime.UtcNow;
                        entity.IsDeleted = false;

                        // Get ClientId from DTO
                        var clientIdProp = typeof(TDto).GetProperty("ClientId");
                        var clientId = clientIdProp?.GetValue(createDto) as string;
                        entity.ClientId = clientId;

                        _dbContext.Set<TEntity>().Add(entity);

                        // Save immediately to generate the int ID
                        await _dbContext.SaveChangesAsync();

                        // Get the generated server ID
                        var serverId = (int)idProperty.GetValue(entity)!;

                        result.Created.Add(new
                        {
                            clientId = clientId,
                            serverId = serverId.ToString(),
                            status = "created"
                        });
                    }
                    catch (Exception ex)
                    {
                        var clientIdProp = typeof(TDto).GetProperty("ClientId");
                        var clientId = clientIdProp?.GetValue(createDto) as string;

                        result.Errors.Add(new
                        {
                            clientId = clientId,
                            error = ex.Message
                        });
                        _logger.LogError($"Error creating {entityTypeName}: {ex.Message}");
                    }
                }

                // Process UPDATE operations
                foreach (var updateDto in changes.Updated)
                {
                    try
                    {
                        // Parse string Id to int
                        var idProp = typeof(TDto).GetProperty("Id");
                        var idString = idProp?.GetValue(updateDto) as string;

                        if (string.IsNullOrEmpty(idString) || !int.TryParse(idString, out int entityId))
                        {
                            result.Errors.Add(new { id = idString, error = "Invalid ID format" });
                            continue;
                        }

                        var entity = await _dbContext.Set<TEntity>()
                            .FirstOrDefaultAsync(e => (int)idProperty.GetValue(e)! == entityId);

                        if (entity == null)
                        {
                            result.Errors.Add(new { id = idString, error = "Entity not found" });
                            continue;
                        }

                        // Conflict detection: Check if server version is newer
                        var clientVersionProp = typeof(TDto).GetProperty("ClientVersion");
                        var clientVersion = clientVersionProp?.GetValue(updateDto) as ClientVersionInfo;

                        if (clientVersion != null && entity.UpdatedAt > clientVersion.PreviousUpdatedAt)
                        {
                            // Conflict detected - server version is newer
                            result.Conflicts.Add(new
                            {
                                id = idString,
                                conflict = "version_mismatch",
                                clientVersion = clientVersion.PreviousUpdatedAt,
                                serverVersion = entity.UpdatedAt,
                                resolution = "LAST_WRITE_WINS",
                                message = "Server version is newer. Client should pull latest data."
                            });
                            continue;
                        }

                        // Map DTO to entity (AutoMapper handles the mapping)
                        _mapper.Map(updateDto, entity);

                        // UpdatedAt will be set automatically by SaveChangesAsync override

                        result.Updated.Add(new
                        {
                            id = idString,
                            status = "updated",
                            serverUpdatedAt = DateTime.UtcNow
                        });
                    }
                    catch (Exception ex)
                    {
                        var idProp = typeof(TDto).GetProperty("Id");
                        var idString = idProp?.GetValue(updateDto) as string;

                        result.Errors.Add(new { id = idString, error = ex.Message });
                        _logger.LogError($"Error updating {entityTypeName}: {ex.Message}");
                    }
                }

                // Process DELETE operations (soft delete)
                foreach (var deleteDto in changes.Deleted)
                {
                    try
                    {
                        if (!int.TryParse(deleteDto.Id, out int entityId))
                        {
                            result.Errors.Add(new { id = deleteDto.Id, error = "Invalid ID format" });
                            continue;
                        }

                        var entity = await _dbContext.Set<TEntity>()
                            .FirstOrDefaultAsync(e => (int)idProperty.GetValue(e)! == entityId);

                        if (entity == null)
                        {
                            result.Errors.Add(new { id = deleteDto.Id, error = "Entity not found" });
                            continue;
                        }

                        // Perform soft delete (SaveChangesAsync will handle this)
                        _dbContext.Set<TEntity>().Remove(entity);

                        result.Deleted.Add(new
                        {
                            id = deleteDto.Id,
                            status = "deleted"
                        });
                    }
                    catch (Exception ex)
                    {
                        result.Errors.Add(new { id = deleteDto.Id, error = ex.Message });
                        _logger.LogError($"Error deleting {entityTypeName}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing {entityTypeName}: {ex.Message}", ex);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Processor B: Handles entities with GUID primary keys (Issue, etc.)
        /// - Create: Generates Guid.NewGuid(), no need to save immediately
        /// - Update: Parses string Id to Guid, finds entity, updates with conflict detection
        /// - Delete: Parses string Id to Guid, finds entity, performs soft delete
        /// Uses reflection to access the Id property since ISyncable doesn't include PK
        /// </summary>
        private async Task<SyncResultDetails> ProcessGuidEntitiesAsync<TEntity, TDto>(
            EntityChanges<TDto> changes,
            string entityTypeName,
            string userId)
            where TEntity : class, ISyncable, new()
            where TDto : class
        {
            var result = new SyncResultDetails();

            try
            {
                // Get the Id property using reflection
                var idProperty = typeof(TEntity).GetProperty("Id");
                if (idProperty == null || idProperty.PropertyType != typeof(Guid))
                {
                    throw new InvalidOperationException($"Entity {typeof(TEntity).Name} must have a 'Guid Id' property");
                }

                // Process CREATE operations
                foreach (var createDto in changes.Created)
                {
                    try
                    {
                        var entity = _mapper.Map<TEntity>(createDto);

                        // Generate new Guid manually
                        var newGuid = Guid.NewGuid();
                        idProperty.SetValue(entity, newGuid);

                        // Ensure sync fields are set
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.UpdatedAt = DateTime.UtcNow;
                        entity.IsDeleted = false;

                        // Get ClientId from DTO
                        var clientIdProp = typeof(TDto).GetProperty("ClientId");
                        var clientId = clientIdProp?.GetValue(createDto) as string;
                        entity.ClientId = clientId;

                        _dbContext.Set<TEntity>().Add(entity);

                        // No need to save immediately for Guid - it's already set

                        result.Created.Add(new
                        {
                            clientId = clientId,
                            serverId = newGuid.ToString(),
                            status = "created"
                        });
                    }
                    catch (Exception ex)
                    {
                        var clientIdProp = typeof(TDto).GetProperty("ClientId");
                        var clientId = clientIdProp?.GetValue(createDto) as string;

                        result.Errors.Add(new
                        {
                            clientId = clientId,
                            error = ex.Message
                        });
                        _logger.LogError($"Error creating {entityTypeName}: {ex.Message}");
                    }
                }

                // Process UPDATE operations
                foreach (var updateDto in changes.Updated)
                {
                    try
                    {
                        // Parse string Id to Guid
                        var idProp = typeof(TDto).GetProperty("Id");
                        var idString = idProp?.GetValue(updateDto) as string;

                        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out Guid entityId))
                        {
                            result.Errors.Add(new { id = idString, error = "Invalid GUID format" });
                            continue;
                        }

                        var entity = await _dbContext.Set<TEntity>()
                            .FirstOrDefaultAsync(e => (Guid)idProperty.GetValue(e)! == entityId);

                        if (entity == null)
                        {
                            result.Errors.Add(new { id = idString, error = "Entity not found" });
                            continue;
                        }

                        // Conflict detection: Check if server version is newer
                        var clientVersionProp = typeof(TDto).GetProperty("ClientVersion");
                        var clientVersion = clientVersionProp?.GetValue(updateDto) as ClientVersionInfo;

                        if (clientVersion != null && entity.UpdatedAt > clientVersion.PreviousUpdatedAt)
                        {
                            // Conflict detected - server version is newer
                            result.Conflicts.Add(new
                            {
                                id = idString,
                                conflict = "version_mismatch",
                                clientVersion = clientVersion.PreviousUpdatedAt,
                                serverVersion = entity.UpdatedAt,
                                resolution = "LAST_WRITE_WINS",
                                message = "Server version is newer. Client should pull latest data."
                            });
                            continue;
                        }

                        // Map DTO to entity (AutoMapper handles the mapping)
                        _mapper.Map(updateDto, entity);

                        // UpdatedAt will be set automatically by SaveChangesAsync override

                        result.Updated.Add(new
                        {
                            id = idString,
                            status = "updated",
                            serverUpdatedAt = DateTime.UtcNow
                        });
                    }
                    catch (Exception ex)
                    {
                        var idProp = typeof(TDto).GetProperty("Id");
                        var idString = idProp?.GetValue(updateDto) as string;

                        result.Errors.Add(new { id = idString, error = ex.Message });
                        _logger.LogError($"Error updating {entityTypeName}: {ex.Message}");
                    }
                }

                // Process DELETE operations (soft delete)
                foreach (var deleteDto in changes.Deleted)
                {
                    try
                    {
                        if (!Guid.TryParse(deleteDto.Id, out Guid entityId))
                        {
                            result.Errors.Add(new { id = deleteDto.Id, error = "Invalid GUID format" });
                            continue;
                        }

                        var entity = await _dbContext.Set<TEntity>()
                            .FirstOrDefaultAsync(e => (Guid)idProperty.GetValue(e)! == entityId);

                        if (entity == null)
                        {
                            result.Errors.Add(new { id = deleteDto.Id, error = "Entity not found" });
                            continue;
                        }

                        // Perform soft delete (SaveChangesAsync will handle this)
                        _dbContext.Set<TEntity>().Remove(entity);

                        result.Deleted.Add(new
                        {
                            id = deleteDto.Id,
                            status = "deleted"
                        });
                    }
                    catch (Exception ex)
                    {
                        result.Errors.Add(new { id = deleteDto.Id, error = ex.Message });
                        _logger.LogError($"Error deleting {entityTypeName}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing {entityTypeName}: {ex.Message}", ex);
                throw;
            }

            return result;
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
                var query = _dbContext.Issues
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
                var query = _dbContext.People
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
                var query = _dbContext.Families
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
                        f.FamilyCatgoryId,
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
