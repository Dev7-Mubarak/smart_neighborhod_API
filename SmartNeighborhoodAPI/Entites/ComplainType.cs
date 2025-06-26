namespace OurProjectSmartNeiborhood.Entites
{
    public class ConflictType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Conflict> Conflicts { get; set; }
    }
}
