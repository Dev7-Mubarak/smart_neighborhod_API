namespace SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers
{ 
    public class CustomTeamMemberDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime DateOfJoin { get; set; }
        public int TeamRoleId { get; set; }
        public string TeamRoleName { get; set; }
    }
}
