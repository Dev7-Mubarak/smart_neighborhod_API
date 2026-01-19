using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using OurProjectSmartNeiborhood.Entites;

namespace SmartNeighborhoodAPI.Entites
{
    public class ResidentialUnit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int ResidentialNeighborhoodId { get; set; }
        public ResidentialNeighborhood ResidentialNeighborhood { get; set; } = null!;

        public string UnitManagerId { get; set; } = string.Empty;
        public AppUser UnitManager { get; set; } = null!;

        public ICollection<Block> Blocks { get; set; } = new List<Block>();

    }
}
