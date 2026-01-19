namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class ResidentialUnitDashboardDto
    {
        public int TotalUnits { get; set; }
        public int TotalBlocks { get; set; }
        public List<UnitStatsDto> Units { get; set; } = new();
    }

    public class UnitStatsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UnitManagerId { get; set; } = string.Empty;
        public string UnitManagerName { get; set; } = string.Empty;
        public int BlockCount { get; set; }
    }
}
