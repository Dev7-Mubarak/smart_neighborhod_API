using System.Text.Json.Serialization;
using SmartNeighborhoodAPI.Entites;

namespace OurProjectSmartNeiborhood.Entites
{
    public class FamilyMember
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int FamilyId { get; set; }
        [JsonIgnore]
        public Family Family { get; set; }
        public int MemberTypeId { get; set; }
        [JsonIgnore]
        public MemberType MemberType { get; set; }
    }
}
