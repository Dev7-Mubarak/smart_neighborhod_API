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
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int BlocksCount { get; set; }
    }
}
