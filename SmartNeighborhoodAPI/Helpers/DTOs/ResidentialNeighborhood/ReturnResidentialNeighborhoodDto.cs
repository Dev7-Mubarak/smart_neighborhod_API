namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class ReturnResidentialNeighborhoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string NeighborhoodManagerId { get; set; } = string.Empty;
        public string NeighborhoodManagerName { get; set; } = string.Empty;
    }
}
