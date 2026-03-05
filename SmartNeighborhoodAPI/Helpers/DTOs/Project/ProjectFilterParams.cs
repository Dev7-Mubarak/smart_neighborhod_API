using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.DTOs;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Project
{
    /// <summary>
    /// Query-string parameters for the GET /api/v1/projects endpoint.
    /// Every property is optional — only non-null values are applied as WHERE clauses.
    /// </summary>
    public class ProjectFilterParams : PaginationParams
    {
        /// <summary>Full-text search against project name.</summary>
        public string? Search { get; set; }

        /// <summary>Filter by project category.</summary>
        public int? ProjectCategoryId { get; set; }

        /// <summary>Filter by project status enum.</summary>
        public ProjectStatus? ProjectStatus { get; set; }

        /// <summary>Filter by project priority enum.</summary>
        public ProjectPriority? ProjectPriority { get; set; }

        /// <summary>Start date range — from (inclusive).</summary>
        public DateTime? From { get; set; }

        /// <summary>Start date range — to (inclusive).</summary>
        public DateTime? To { get; set; }

        /// <summary>Column to sort by: "name" | "startDate" | "budget". Defaults to "name".</summary>
        public string? SortBy { get; set; }

        /// <summary>"asc" or "desc". Defaults to "asc".</summary>
        public string? SortOrder { get; set; } = "asc";
    }
}
