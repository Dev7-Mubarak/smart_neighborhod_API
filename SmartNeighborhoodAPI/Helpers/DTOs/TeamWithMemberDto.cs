namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class TeamWithMembersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<TeamMemberWithPersonDto> TeamMembers { get; set; }
    }

   


}
