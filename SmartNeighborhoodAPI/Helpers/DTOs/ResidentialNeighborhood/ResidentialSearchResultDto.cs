namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class ResidentialSearchResultDto
    {
        public int NeighborhoodId { get; set; }
        public string NeighborhoodName { get; set; } = string.Empty;

        public int? UnitId { get; set; }
        public string? UnitName { get; set; }

        public int? BlockId { get; set; }
        public string? BlockName { get; set; }
    }
}
