using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class TeamMemberWithPersonDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int MemberId { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string MemberJob { get; set; }
        public PersonDto Person { get; set; }
    }
}
