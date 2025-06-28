namespace OurProjectSmartNeiborhood.Entites
{
    public class ProjectFamily
    {
        public int Id { get; set; }

        public int ProjectID { get; set; }
        public Project Project { get; set; }

        public int FamilyID { get; set; }
        public Family Family { get; set; }
    }
}
