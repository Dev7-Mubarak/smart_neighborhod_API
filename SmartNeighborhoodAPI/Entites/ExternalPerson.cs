namespace SmartNeighborhoodAPI.Entites
{
    public class ExternalPerson
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string IdentityNumber { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        public ICollection<ConfilctParty> ConfilctParties { get; set; }
    }
}
