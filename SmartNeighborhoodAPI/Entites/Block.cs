using SmartNeighborhoodAPI.Entites;
using System.Text.Json.Serialization;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Block
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
    }
}
