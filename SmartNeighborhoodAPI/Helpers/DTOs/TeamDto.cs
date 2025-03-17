namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class TeamDto
    {
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Project id is required")]
        public int ProjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
