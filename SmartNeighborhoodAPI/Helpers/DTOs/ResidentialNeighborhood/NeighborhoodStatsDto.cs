namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class NeighborhoodStatsDto
    {
        public int NeighborhoodId { get; set; }
        public string NeighborhoodName { get; set; } = string.Empty;

        public int UnitsCount { get; set; }
        public int BlocksCount { get; set; }
        public string ManagerId { get; set; } = string.Empty;
        public string ManagerName { get; set; } = string.Empty;
    }
}
