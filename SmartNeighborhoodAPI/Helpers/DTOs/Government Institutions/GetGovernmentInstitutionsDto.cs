
using SmartNeighborhoodAPI.Helpers.DTOs.Contact;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Auth
{
    public class GetGovernmentInstitutionsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<GetContactDto> Contacts { get; set; } = Array.Empty<GetContactDto>();
    }
}
