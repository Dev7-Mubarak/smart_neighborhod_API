namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class ResidentialUnitSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UnitManagerId { get; set; } = string.Empty;
        public string UnitManagerName { get; set; } = string.Empty;
    }
}
