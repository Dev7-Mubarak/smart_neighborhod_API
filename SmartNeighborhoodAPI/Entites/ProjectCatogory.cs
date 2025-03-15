namespace OurProjectSmartNeiborhood.Entites
{
    public class ProjectCatogory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Project> Projects { get; set; }

    }
}
