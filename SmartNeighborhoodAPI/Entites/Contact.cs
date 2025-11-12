namespace SmartNeighborhoodAPI.Entites
{
    public class Contact
    {
        public int Id { get; set; }
        public int AuthorityId { get; set; }
        public GovernmentInstitution GovernmentInstitution { get; set; }

        public string Name { get; set; }
        public string Job { get; set; }
        public string Phone { get; set; }
    }
}
