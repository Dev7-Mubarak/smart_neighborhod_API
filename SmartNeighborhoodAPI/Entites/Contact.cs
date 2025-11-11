namespace SmartNeighborhoodAPI.Entites
{
    public class Contact
    {
        public int Id { get; set; }
        public int AuthorityId { get; set; }
        public Authority Authority { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string Job { get; set; }
        public string Phone { get; set; } = null!;
    }
}
