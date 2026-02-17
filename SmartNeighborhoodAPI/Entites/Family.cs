using System.Text.Json.Serialization;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Family : ISyncable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string? FamilyNotes { get; set; }
        public int FamilyCatgoryId { get; set; }
        public FamilyCatgory FamilyCatgory { get; set; }
        public HousingType HousingType { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; }
        public int BlockId { get; set; }
        public Block Block { get; set; }

        public ICollection<ProjectFamily> ProjectFamilies { get; set; }

        // ISyncable implementation :-
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? ClientId { get; set; }
    }


}
