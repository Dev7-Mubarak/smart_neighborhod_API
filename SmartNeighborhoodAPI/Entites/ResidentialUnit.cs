using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Entites
{
    public class ResidentialUnit : ISyncable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int ResidentialNeighborhoodId { get; set; }
        public ResidentialNeighborhood ResidentialNeighborhood { get; set; } = null!;

        public string UnitManagerId { get; set; } = string.Empty;
        public AppUser UnitManager { get; set; } = null!;

        public ICollection<Block> Blocks { get; set; } = new List<Block>();

        // ISyncable implementation
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? ClientId { get; set; }
    }
}
