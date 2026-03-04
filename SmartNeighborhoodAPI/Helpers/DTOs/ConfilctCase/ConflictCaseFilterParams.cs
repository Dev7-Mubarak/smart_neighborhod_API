using SmartNeighborhoodAPI.Helpers.DTOs;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase
{
    /// <summary>
    /// Query-string parameters for the GET /api/v1/conflict-cases endpoint.
    /// Every property is optional — only non-null values are applied as WHERE clauses.
    /// </summary>
    public class ConflictCaseFilterParams : PaginationParams
    {
        /// <summary>Full-text search against case title.</summary>
        public string? Search { get; set; }

        /// <summary>Filter by resolved / unresolved status.</summary>
        public bool? IsResolved { get; set; }

        /// <summary>Filter by a specific conflict type.</summary>
        public int? ConflictTypeId { get; set; }

        /// <summary>Filter by the block where the conflict occurred.</summary>
        public int? BlockId { get; set; }

        /// <summary>Filter by the assigned manager's identity (AppUser.Id).</summary>
        public string? ManagerId { get; set; }

        /// <summary>Session date range — start (inclusive).</summary>
        public DateTime? From { get; set; }

        /// <summary>Session date range — end (inclusive).</summary>
        public DateTime? To { get; set; }

        /// <summary>Column to sort by: "sessionDate" | "title". Defaults to "sessionDate".</summary>
        public string? SortBy { get; set; }

        /// <summary>"asc" or "desc". Defaults to "asc".</summary>
        public string? SortOrder { get; set; } = "asc";
    }
}
