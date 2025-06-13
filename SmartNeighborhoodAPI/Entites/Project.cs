using SmartNeighborhoodAPI.Entites.Enums;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ManagerId { get; set; }
        public Person Manager { get; set; }
        public int ProjectCatogoryId { get; set; }
        public ProjectCatogory ProjectCatogory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public double? Budget { get; set; }
        public ProjectPriority ProjectPriority { get; set; }
        public List<Team>? Teams { get; set; }
    }
}
