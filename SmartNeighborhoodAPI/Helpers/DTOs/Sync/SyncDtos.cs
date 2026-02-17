using System;
using System.Collections.Generic;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Sync
{
    /// <summary>
    /// Request body for the Pull Changes endpoint.
    /// Client sends this to download all server changes since last sync.
    /// </summary>
    public class SyncPullRequest
    {
        /// <summary>
        /// Last successful sync timestamp from server.
        /// Server returns only records modified after this time.
        /// Format: ISO 8601 (e.g., "2024-02-01T10:30:00Z")
        /// </summary>
        public DateTime SyncToken { get; set; }

        /// <summary>
        /// Comma-separated list of entity type names to sync.
        /// If empty, syncs all entity types.
        /// Examples: "issues", "persons,families", "issues,persons,families,blocks"
        /// </summary>
        public string? EntityTypes { get; set; }

        /// <summary>
        /// Maximum number of records to return per entity type.
        /// Default: 1000. Maximum: 5000.
        /// Used for pagination across multiple sync requests.
        /// </summary>
        public int BatchSize { get; set; } = 1000;

        /// <summary>
        /// Include soft-deleted records in response.
        /// When true, returns records with IsDeleted=true and DeletedAt timestamp.
        /// Default: true
        /// </summary>
        public bool IncludeDeleted { get; set; } = true;
    }

    /// <summary>
    /// Response for Pull Changes endpoint.
    /// Contains all changes that occurred on server since last sync.
    /// </summary>
    public class SyncPullResponse
    {
        /// <summary>
        /// Timestamp when client made the sync request.
        /// </summary>
        public DateTime RequestedAt { get; set; }

        /// <summary>
        /// Current server time. Use this as sync_token for next pull request.
        /// </summary>
        public DateTime ServerTimestamp { get; set; }

        /// <summary>
        /// Suggested timestamp for next pull request.
        /// Usually equals ServerTimestamp.
        /// </summary>
        public DateTime NextSyncToken { get; set; }

        /// <summary>
        /// Whether more data exists beyond current batch.
        /// Client should repeat pull request if true, using NextSyncToken.
        /// </summary>
        public bool HasMore { get; set; } = false;

        /// <summary>
        /// Dictionary mapping entity type names to lists of changed records.
        /// Key examples: "issues", "persons", "families"
        /// Value: List of entity records as anonymous objects/JSON.
        ///
        /// Example structure:
        /// {
        ///   "issues": [ {...}, {...} ],
        ///   "persons": [ {...}, {...} ],
        ///   "families": [ {...} ]
        /// }
        /// </summary>
        public Dictionary<string, List<dynamic>> Changes { get; set; } = new();
    }

    /// <summary>
    /// Changes for a single entity type (created, updated, deleted).
    /// </summary>
    public class EntityChanges<T> where T : class
    {
        /// <summary>
        /// Records newly created by client while offline.
        /// Server will generate new server-side IDs and return mapping.
        /// </summary>
        public List<T> Created { get; set; } = new();

        /// <summary>
        /// Records modified by client while offline.
        /// Contains partial or full record data to update.
        /// </summary>
        public List<T> Updated { get; set; } = new();

        /// <summary>
        /// Records deleted by client while offline.
        /// Server will soft-delete these records.
        /// </summary>
        public List<DeletedItemDto> Deleted { get; set; } = new();
    }

    /// <summary>
    /// Represents a deleted item in the sync push request.
    /// </summary>
    public class DeletedItemDto
    {
        /// <summary>Server-side ID of the deleted record.</summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>Timestamp when client marked this record as deleted.</summary>
        public DateTime DeletedAt { get; set; }
    }

    /// <summary>
    /// Container for all changes across all entity types.
    /// Used in SyncPushRequest.
    /// </summary>
    public class SyncChanges
    {
        public EntityChanges<IssueChangeDto>? Issues { get; set; }
        public EntityChanges<PersonChangeDto>? Persons { get; set; }
        public EntityChanges<FamilyChangeDto>? Families { get; set; }
        public EntityChanges<FamilyMemberChangeDto>? FamilyMembers { get; set; }
        public EntityChanges<BlockChangeDto>? Blocks { get; set; }
        public EntityChanges<ResidentialNeighborhoodChangeDto>? ResidentialNeighborhoods { get; set; }
        public EntityChanges<ConflictCaseChangeDto>? ConflictCases { get; set; }
        public EntityChanges<ProjectChangeDto>? Projects { get; set; }
        // Add other entity types as needed
    }

    /// <summary>
    /// Request body for Push Changes endpoint.
    /// Client sends offline changes to server for processing.
    /// </summary>
    public class SyncPushRequest
    {
        /// <summary>
        /// Unique identifier for this mobile client/device.
        /// Format: UUID v4
        /// Used for tracking which device made changes and for idempotency.
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// When this sync request was initiated by client.
        /// Helps server determine request freshness.
        /// </summary>
        public DateTime RequestedAt { get; set; }

        /// <summary>
        /// Last SyncToken (ServerTimestamp) received from previous pull.
        /// Server uses to detect if client has missed any intervening updates.
        /// </summary>
        public DateTime LastSyncToken { get; set; }

        /// <summary>
        /// Unique key for idempotency.
        /// If client retries with same key, server returns cached response.
        /// Format: UUID v4. Generated by client, not server.
        /// </summary>
        public string? IdempotencyKey { get; set; }

        /// <summary>
        /// All changes (creations, updates, deletions) across all entity types.
        /// </summary>
        public SyncChanges Changes { get; set; } = new();
    }

    /// <summary>
    /// Response for Push Changes endpoint.
    /// Indicates which records were successfully synced and any conflicts/errors.
    /// </summary>
    public class SyncPushResponse
    {
        /// <summary>
        /// When client made the sync push request.
        /// </summary>
        public DateTime RequestedAt { get; set; }

        /// <summary>
        /// When server completed processing the sync push.
        /// </summary>
        public DateTime ProcessedAt { get; set; }

        /// <summary>
        /// Current server timestamp for next sync pull.
        /// </summary>
        public DateTime ServerTimestamp { get; set; }

        /// <summary>
        /// Overall sync result: "success", "partial_success", "error"
        /// "success" = no conflicts or errors
        /// "partial_success" = some records failed or had conflicts
        /// "error" = entire sync failed, no records processed
        /// </summary>
        public string SyncStatus { get; set; } = "success";

        /// <summary>Total records successfully processed (created + updated + deleted).</summary>
        public int TotalProcessed { get; set; }

        /// <summary>Total records that encountered conflicts.</summary>
        public int TotalConflicts { get; set; }

        /// <summary>
        /// Results for each entity type.
        /// Key: entity type name (e.g., "issues", "persons")
        /// Value: SyncResultDetails with created/updated/deleted/conflict/error counts
        /// </summary>
        public Dictionary<string, SyncResultDetails> Results { get; set; } = new();
    }

    /// <summary>
    /// Detailed results for a single entity type in sync push response.
    /// </summary>
    public class SyncResultDetails
    {
        /// <summary>
        /// Successfully created records with server-assigned IDs.
        /// Each object contains: clientId, serverId, status
        /// </summary>
        public List<dynamic> Created { get; set; } = new();

        /// <summary>
        /// Successfully updated records.
        /// Each object contains: id, status, serverUpdatedAt
        /// </summary>
        public List<dynamic> Updated { get; set; } = new();

        /// <summary>
        /// Successfully deleted records.
        /// Each object contains: id, status
        /// </summary>
        public List<dynamic> Deleted { get; set; } = new();

        /// <summary>
        /// Records with synchronization conflicts.
        /// Client should fetch updated server version and retry.
        /// Each object contains: id, conflict, clientVersion, serverVersion, resolution, serverData
        /// </summary>
        public List<dynamic> Conflicts { get; set; } = new();

        /// <summary>
        /// Records that failed to process.
        /// Each object contains: id, error
        /// </summary>
        public List<dynamic> Errors { get; set; } = new();
    }

    /// <summary>
    /// Sync status check response (optional endpoint).
    /// Client uses to determine sync health without performing actual sync.
    /// </summary>
    public class SyncStatusResponse
    {
        /// <summary>Whether sync service is available for this user.</summary>
        public bool IsSyncAvailable { get; set; } = true;

        /// <summary>Last successful sync datetime.</summary>
        public DateTime? LastSuccessfulSync { get; set; }

        /// <summary>Estimated number of pending changes on server.</summary>
        public int PendingChangesEstimate { get; set; }

        /// <summary>Current backend sync API version.</summary>
        public string ServerVersion { get; set; } = "2.0";

        /// <summary>Minimum client version required for compatibility.</summary>
        public string MinimumClientVersion { get; set; } = "1.5";

        /// <summary>Optional recommendations for client (e.g., "Update required", "Clear cache").</summary>
        public List<string> Recommendations { get; set; } = new();
    }

    // DTO classes for specific entity changes
    // All DTOs use string? Id to handle both int and Guid primary keys from mobile clients

    /// <summary>
    /// Issue change DTO - supports Guid primary key
    /// </summary>
    public class IssueChangeDto
    {
        // Id can be null for create operations, or string representation of Guid for update/delete
        public string? Id { get; set; }

        // ClientId is used for mapping new offline records
        public string? ClientId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? ReporterId { get; set; }
        public string? AssigneeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientVersionInfo? ClientVersion { get; set; }
    }

    /// <summary>
    /// Person change DTO - supports int primary key
    /// </summary>
    public class PersonChangeDto
    {
        // Id can be null for create operations, or string representation of int for update/delete
        public string? Id { get; set; }

        // ClientId is used for mapping new offline records
        public string? ClientId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string ThirdName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool? IsWhatsapp { get; set; }
        public bool? IsContactNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodType { get; set; }
        public string? MaritalStatus { get; set; }
        public string? OccupationStatus { get; set; }
        public string? PersonType { get; set; }
        public string? Job { get; set; }
        public string? NationalId { get; set; }
        public string? VehicleType { get; set; }
        public string? VehicleRegistrationNumber { get; set; }
        public string? ResidencyStatus { get; set; }
        public bool? HasChronicDiseases { get; set; }
        public string? ChronicDiseasesNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientVersionInfo? ClientVersion { get; set; }
    }

    /// <summary>
    /// Family change DTO - supports int primary key
    /// </summary>
    public class FamilyChangeDto
    {
        // Id can be null for create operations, or string representation of int for update/delete
        public string? Id { get; set; }

        // ClientId is used for mapping new offline records
        public string? ClientId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? FamilyNotes { get; set; }
        public string FamilyCategoryId { get; set; } = string.Empty;
        public string HousingType { get; set; } = string.Empty;
        public string BlockId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientVersionInfo? ClientVersion { get; set; }
    }

    public class FamilyMemberChangeDto
    {
        public string? Id { get; set; }
        public string? ClientId { get; set; }
        public string FamilyId { get; set; } = string.Empty;
        public string PersonId { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientVersionInfo? ClientVersion { get; set; }
    }

    public class BlockChangeDto
    {
        public string? Id { get; set; }
        public string? ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ResidentialUnitId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientVersionInfo? ClientVersion { get; set; }
    }

    public class ResidentialNeighborhoodChangeDto
    {
        public string? Id { get; set; }
        public string? ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientVersionInfo? ClientVersion { get; set; }
    }

    public class ConflictCaseChangeDto
    {
        public string? Id { get; set; }
        public string? ClientId { get; set; }
        public string FirstPartyId { get; set; } = string.Empty;
        public string SecondPartyId { get; set; } = string.Empty;
        public string ConflictTypeId { get; set; } = string.Empty;
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientVersionInfo? ClientVersion { get; set; }
    }

    public class ProjectChangeDto
    {
        public string? Id { get; set; }
        public string? ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientVersionInfo? ClientVersion { get; set; }
    }

    /// <summary>
    /// Version metadata for conflict detection during updates.
    /// Client provides information about the version it's updating.
    /// </summary>
    public class ClientVersionInfo
    {
        /// <summary>
        /// The UpdatedAt timestamp from the client's last known version.
        /// Server compares to current server UpdatedAt to detect conflicts.
        /// </summary>
        public DateTime PreviousUpdatedAt { get; set; }

        /// <summary>
        /// Optional hash of the entire previous record for additional validation.
        /// Computed by client as SHA256 hash of serialized record.
        /// </summary>
        public string? PreviousHash { get; set; }
    }
}
