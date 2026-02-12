using SmartNeighborhoodAPI.Helpers.DTOs.Sync;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartNeighborhoodAPI.Interfaces
{
    /// <summary>
    /// Service interface for offline-first data synchronization.
    /// Handles pulling changes from server and pushing changes to server.
    /// </summary>
    public interface ISyncService
    {
        /// <summary>
        /// Retrieves all changes from server since the specified sync token.
        /// Used by mobile client to download server-side updates.
        ///
        /// Returns records that were:
        /// - Created or modified after syncToken
        /// - Not deleted (unless includeDeleted=true)
        ///
        /// Supports pagination via batchSize and hasMore flag.
        /// </summary>
        /// <param name="syncToken">Last successful sync timestamp from client</param>
        /// <param name="entityTypes">Comma-separated entity types (e.g., "issues,persons")</param>
        /// <param name="batchSize">Max records per entity type (1-5000)</param>
        /// <param name="includeDeleted">Include soft-deleted records</param>
        /// <param name="userId">ID of authenticated user performing sync</param>
        /// <returns>SyncPullResponse with server changes</returns>
        Task<SyncPullResponse> PullChangesAsync(
            DateTime syncToken,
            string? entityTypes = null,
            int batchSize = 1000,
            bool includeDeleted = true,
            string? userId = null);

        /// <summary>
        /// Processes batch of offline changes from mobile client.
        /// Handles:
        /// - Inserting new records (with clientId to serverId mapping)
        /// - Updating existing records (with conflict detection)
        /// - Deleting records (soft deletes)
        ///
        /// Uses database transaction - all succeed or all fail.
        /// Applies "Last Write Wins" conflict resolution based on UpdatedAt timestamps.
        /// </summary>
        /// <param name="request">SyncPushRequest with created/updated/deleted changes</param>
        /// <param name="userId">ID of authenticated user performing sync</param>
        /// <param name="idempotencyKey">Unique key to prevent duplicate processing</param>
        /// <returns>SyncPushResponse indicating success/conflicts/errors</returns>
        Task<SyncPushResponse> PushChangesAsync(
            SyncPushRequest request,
            string userId,
            string? idempotencyKey = null);

        /// <summary>
        /// Checks sync health status without performing actual sync.
        /// Client calls to determine if sync is available and any needed actions.
        /// </summary>
        /// <param name="userId">ID of authenticated user</param>
        /// <returns>SyncStatusResponse with availability and recommendations</returns>
        Task<SyncStatusResponse> GetSyncStatusAsync(string userId);

        /// <summary>
        /// Gets changes for a specific entity type since last sync.
        /// Used internally by PullChangesAsync.
        ///
        /// Query optimization is critical:
        /// - Use UpdatedAt index for timestamp range queries
        /// - Filter IsDeleted = false in base query
        /// - Order by UpdatedAt ascending (oldest first)
        /// </summary>
        /// <param name="entityType">Name of entity type (e.g., "issues", "persons")</param>
        /// <param name="since">Only return records modified after this time</param>
        /// <param name="batchSize">Maximum records to return</param>
        /// <param name="includeDeleted">Include soft-deleted records</param>
        /// <param name="userId">User performing sync (for access control)</param>
        /// <returns>List of entities as anonymous objects</returns>
        Task<List<dynamic>> GetChangedEntitiesAsync(
            string entityType,
            DateTime since,
            int batchSize,
            bool includeDeleted,
            string userId);

        /// <summary>
        /// Resolves conflicts between client and server versions.
        /// Strategy: "Last Write Wins" - most recent UpdatedAt timestamp wins.
        ///
        /// Returns conflict metadata indicating:
        /// - What conflicted
        /// - How it was resolved
        /// - Server's winning version
        /// </summary>
        /// <param name="clientVersion">Client's version of the record</param>
        /// <param name="serverVersion">Server's current version of the record</param>
        /// <returns>Conflict resolution result with metadata</returns>
        dynamic ResolveConflict(object clientVersion, object serverVersion);

        /// <summary>
        /// Maps client-generated IDs to server-generated IDs after creation sync.
        /// Mobile app needs this mapping to update local database with server IDs.
        ///
        /// Example:
        /// Client created issue with clientId="uuid-123"
        /// Server assigned serverId="550e8400-e29b-41d4..."
        /// This method returns: { clientId: "uuid-123", serverId: "550e8400..." }
        /// </summary>
        /// <param name="entityType">Type of entity created</param>
        /// <param name="clientId">Client-generated ID</param>
        /// <param name="serverId">Server-assigned ID</param>
        /// <returns>Mapping object for client to update local database</returns>
        Task<dynamic> MapClientIdToServerIdAsync(string entityType, string clientId, Guid serverId);
    }
}
