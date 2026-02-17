using SmartNeighborhoodAPI.Entites;
using System.Text.Json.Serialization;
using SmartNeighborhoodAPI.Interfaces;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Block : ISyncable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ResidentialUnitId { get; set; }
        public ResidentialUnit ResidentialUnit { get; set; }

        public string BlockManagerId { get; set; }
        public AppUser BlockManager { get; set; }

        public ICollection<Family> Families { get; set; } = new List<Family>();
        public ICollection<ProjectBlock> ProjectBlocks { get; set; } = new List<ProjectBlock>();
        public ICollection<ConflictCase> ConflictCases { get; set; } = new List<ConflictCase>();

        // ISyncable implementation
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? ClientId { get; set; }
    }
}
