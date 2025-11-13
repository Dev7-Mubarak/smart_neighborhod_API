
using SmartNeighborhoodAPI.Helpers.DTOs.Government_InstitutionsContacts;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Auth
{
    public class GetGovernmentInstitutionsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<GetGovernmentInstitutionContactDto> GovernmentInstitutionContacts { get; set; } = Array.Empty<GetGovernmentInstitutionContactDto>();
    }
}
