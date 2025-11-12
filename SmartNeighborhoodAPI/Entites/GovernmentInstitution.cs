namespace SmartNeighborhoodAPI.Entites
{
    public class GovernmentInstitution
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
