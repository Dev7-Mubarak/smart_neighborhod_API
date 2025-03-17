namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ProjectDto
    {
        [Required(ErrorMessage ="Name is reqired")]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Manager is reqired")]
        public int ManagerId { get; set; }
        [Required(ErrorMessage ="Project Catgory Id is reqired")]
        public int ProjectCatgoryId { get; set; }
        [Required(ErrorMessage ="Start date is reqired")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Procedures { get; set; }
        [Required(ErrorMessage ="Project Status is reqired")]
        [MaxLength(10)]
        public string ProjectStatus { get; set; }
        public double? Budget { get; set; }
        [Required(ErrorMessage ="Projorty is reqired")]
        [MaxLength(30)]
        public string Proiorty { get; set; }
    }
}
