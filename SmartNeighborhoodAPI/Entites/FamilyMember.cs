using System.Text.Json.Serialization;
using SmartNeighborhoodAPI.Entites;

namespace OurProjectSmartNeiborhood.Entites
{
    public class FamilyMember
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }

        public int MemberFamilyRoleId { get; set; }
        public MemberFamilyRole MemberFamilyRole { get; set; }
    }

}
