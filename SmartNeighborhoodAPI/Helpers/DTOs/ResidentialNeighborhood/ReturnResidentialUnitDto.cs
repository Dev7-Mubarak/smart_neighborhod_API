namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class ReturnResidentialUnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string UnitManagerId { get; set; } = string.Empty;
        public string UnitManagerName { get; set; } = string.Empty;

        public List<Block> Blocks { get; set; } = new();
    }
}
