namespace SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers
{
    public class TeamDto
    {
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(30)]
        public required string Name { get; set; }
        public required int TeamLeadId { get; set; }
        public DateTime InJoiedDate { get; set; } = DateTime.Now;
    }
}
