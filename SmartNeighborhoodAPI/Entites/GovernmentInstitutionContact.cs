namespace SmartNeighborhoodAPI.Entites
{
    public class GovernmentInstitutionContact
    {
        public int Id { get; set; }
        public int GovernmentInstitutionId { get; set; }
        public GovernmentInstitution GovernmentInstitution { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Job { get; set; }
        public string Phone { get; set; } = null!;
    }
}
