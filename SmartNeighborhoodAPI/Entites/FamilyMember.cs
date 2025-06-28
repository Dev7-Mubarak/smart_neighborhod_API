using System.Text.Json.Serialization;
using SmartNeighborhoodAPI.Entites;

namespace OurProjectSmartNeiborhood.Entites
{
    public class FamilyMember
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
    }

}
