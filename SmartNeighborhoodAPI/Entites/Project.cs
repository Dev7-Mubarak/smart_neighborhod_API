namespace OurProjectSmartNeiborhood.Entites
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public int ProjectCatgoryId { get; set; }
        public ProjectCatogory ProjectCatogory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Procedures { get; set; }
        public string ProjectStatus { get; set; }
        public double? Budget { get; set; }
        public string Proiorty { get; set; }
        public List<Team> Teams { get; set; }
    }
}
