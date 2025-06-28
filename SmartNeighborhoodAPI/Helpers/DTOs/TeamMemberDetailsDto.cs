namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class TeamMemberDetailsDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public DateTime DateOfJoin { get; set; }
        public int TeamRoleId { get; set; }
        public string TeamRoleName { get; set; }
    }

}
