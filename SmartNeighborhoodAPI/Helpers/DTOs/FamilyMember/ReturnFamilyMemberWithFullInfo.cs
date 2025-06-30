using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember
{
    public class ReturnFamilyMemberWithFullInfo
    {
        public int FamilyMemberId { get; set; }
        public MemberFamilyRole Role { get; set; }
        public PersonDto Person { get; set; }

    }
}
