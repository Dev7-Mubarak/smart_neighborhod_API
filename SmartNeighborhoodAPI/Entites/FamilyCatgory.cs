namespace OurProjectSmartNeiborhood.Entites
{
    public class FamilyCatgory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Family>? Families { get; set; }
    }
}
