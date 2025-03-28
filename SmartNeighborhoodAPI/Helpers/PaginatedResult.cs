namespace SmartNeighborhoodAPI.Helpers
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(List<T> data)
        {
            items = data;
        }
        public List<T> items { get; set; }

        internal PaginatedResult(List<T> items = default, int count = 0, int page = 1, int pageSize = 10)
        {
            this.items = items;
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public static PaginatedResult<T> Success(List<T> items, int count, int page, int pageSize)
        {
            return new(items, count, page, pageSize);
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => PageSize * CurrentPage < TotalCount;
    }
}
