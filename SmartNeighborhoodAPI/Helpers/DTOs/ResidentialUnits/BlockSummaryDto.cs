namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class BlockSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? BlockManagerId { get; set; }
        public string? BlockManagerName { get; set; }
    }
}
