namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    /// <summary>
    /// Base class for all paginated query parameters.
    /// Filters inherit from this to automatically gain PageNumber and PageSize.
    /// </summary>
    public class PaginationParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
