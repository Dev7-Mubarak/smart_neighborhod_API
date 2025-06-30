namespace OurProjectSmartNeiborhood.Entites
{
    public class ProjectFamily
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public int BlockId { get; set; }
        public Block Block { get; set; }
   
    }
}
