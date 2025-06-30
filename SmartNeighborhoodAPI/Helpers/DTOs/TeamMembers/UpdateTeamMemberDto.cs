namespace SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers
{
    public class UpdateTeamMemberDto
    {
        public int TeamId { get; set; }             
        public int PersonId { get; set; }           
        public int TeamRoleId { get; set; }         
        public DateTime DateOfJoin { get; set; }     
    }

}
