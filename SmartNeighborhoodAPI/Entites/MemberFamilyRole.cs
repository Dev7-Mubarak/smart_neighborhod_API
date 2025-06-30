using System.Text.Json.Serialization;

namespace OurProjectSmartNeiborhood.Entites
{
    public class MemberFamilyRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        [JsonIgnore]
        public List<FamilyMember> FamilyMembers { get; set; }
    }
}
