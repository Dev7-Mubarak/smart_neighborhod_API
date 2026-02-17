using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Interfaces;

namespace OurProjectSmartNeiborhood.Entites
{
    public class ConflictCase : ISyncable
    {
        public int Id { get; set; }
        public int FirstPartyId { get; set; }
        public int SecondPartyId { get; set; }
        public int ConflictTypeId { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }
        public DateTime SessionDate { get; set; }

        public bool IsResolved { get; set; }
        [Required]
        public string Title { get; set; }
        public int? BlockId { get; set; }
        public string? ManagerId { get; set; }
        public AppUser? Manager { get; set; }
        public Block Block { get; set; }

        public FamilyMember FirstParty { get; set; }
        public FamilyMember SecondParty { get; set; }
        public ConfilctType ConflictType { get; set; }

        // ISyncable implementation
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? ClientId { get; set; }
    }

}
