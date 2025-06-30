namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ProjectDetailsDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string CategoryName { get; set; }

        public List<TeamDtoDetails> Teams { get; set; }
        public List<ProjectFamilyDtoDetails> ProjectFamilies { get; set; }
    }
}
