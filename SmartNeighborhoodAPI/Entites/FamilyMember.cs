using SmartNeighborhoodAPI.Entites;

namespace OurProjectSmartNeiborhood.Entites
{
    public class FamilyMember
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int? FamilyId { get; set; }
        public AppUser? User { get; set; }
        public int UserId { get; set; }
        public Family Family { get; set; }
        public int MemberTypeId { get; set; }
        public MemberType MemberType { get; set; }
    }
}
