namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class ResidentialDashboardDto
    {
        public int TotalNeighborhoods { get; set; }
        public int TotalUnits { get; set; }
        public int TotalBlocks { get; set; }

        public List<NeighborhoodStatsDto> Neighborhoods { get; set; } = new();
    }
}
