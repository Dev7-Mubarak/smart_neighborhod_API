using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Teams
{
    public class CustomTeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeamMemberDetailsDto> TeamMembers { get; set; }
    }
}
