namespace SmartNeighborhoodAPI.Helpers.DTOs.Project
{
    public class ReturnProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ProjectStatus { get; set; }
        public double? Budget { get; set; }
        public string ProjectPriority { get; set; }
        public CustomPersonDto Manager { get; set; }
        public ProjectCatogory ProjectCatgory { get; set; }
    }
}
