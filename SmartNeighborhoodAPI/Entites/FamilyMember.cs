using System.Text.Json.Serialization;
using SmartNeighborhoodAPI.Interfaces;

namespace OurProjectSmartNeiborhood.Entites
{
    public class FamilyMember : ISyncable
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        [JsonIgnore]
        public Person Person { get; set; }

        public int FamilyId { get; set; }
        [JsonIgnore]
        public Family Family { get; set; }

        public int MemberFamilyRoleId { get; set; }
        [JsonIgnore]
        public MemberFamilyRole MemberFamilyRole { get; set; }
        [JsonIgnore]
        public ICollection<ConflictCase> FirstPartyConflictCases { get; set; }
        public ICollection<ConflictCase> SecondPartyConflictCases { get; set; }

        // ISyncable implementation
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? ClientId { get; set; }
    }

}
