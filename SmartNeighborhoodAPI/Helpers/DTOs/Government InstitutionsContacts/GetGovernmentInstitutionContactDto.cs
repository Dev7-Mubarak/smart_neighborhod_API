namespace SmartNeighborhoodAPI.Helpers.DTOs.Government_InstitutionsContacts
{

    public class GetGovernmentInstitutionContactDto
    {
        public int Id { get; set; }
        public int GovernmentInstitutionId { get; set; }
        public string Name { get; set; } = null!;
        public string? Job { get; set; }
        public string Phone { get; set; } = null!;
    }
}
