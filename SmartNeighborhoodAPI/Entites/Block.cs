using SmartNeighborhoodAPI.Entites;
using System.Text.Json.Serialization;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Block
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ParentBlockId { get; set; }

        public Block ParentBlock { get; set; }

        public ICollection<Block> ChildBlocks { get; set; } = new List<Block>();

        public string ManagerId { get; set; }
        [JsonIgnore]
        public AppUser Manager { get; set; }

        [JsonIgnore]
        public ICollection<Family> Families { get; set; } = new List<Family>();
    }
}
