using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.DTOs;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Person
{
    /// <summary>
    /// Query-string parameters for the GET /api/v1/persons endpoint.
    /// Every property is optional — only non-null values are applied as WHERE clauses.
    /// </summary>
    public class PersonFilterParams : PaginationParams
    {
        /// <summary>Full-text search against first, second, third, or last name.</summary>
        public string? Search { get; set; }

        /// <summary>Filter by gender enum value.</summary>
        public Gender? Gender { get; set; }

        /// <summary>Filter by marital status.</summary>
        public MaritalStatus? MaritalStatus { get; set; }

        /// <summary>Filter by occupation status.</summary>
        public OccupationStatus? OccupationStatus { get; set; }

        /// <summary>Filter residents vs. displaced persons.</summary>
        public ResidencyStatus? ResidencyStatus { get; set; }

        /// <summary>Filter by person type (Citizen, Admin, etc.).</summary>
        public PersonType? PersonType { get; set; }

        /// <summary>When true returns only chronic-disease sufferers; false returns healthy persons.</summary>
        public bool? HasChronicDiseases { get; set; }

        /// <summary>Filter by blood type.</summary>
        public BloodType? BloodType { get; set; }

        /// <summary>Column to sort by: "firstName" | "lastName" | "dateOfBirth". Defaults to "firstName".</summary>
        public string? SortBy { get; set; }

        /// <summary>"asc" or "desc". Defaults to "asc".</summary>
        public string? SortOrder { get; set; } = "asc";
    }
}
