namespace SmartNeighborhoodAPI.Entites
{
    public class GovernmentInstitution
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<GovernmentInstitutionContact> GovernmentInstitutionContacts { get; set; } = new List<GovernmentInstitutionContact>();
    }
}
