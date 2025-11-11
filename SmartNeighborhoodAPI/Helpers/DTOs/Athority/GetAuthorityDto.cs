
namespace SmartNeighborhoodAPI.Helpers.DTOs.Auth
{
    public class GetAuthorityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<GetContactDto> Contacts { get; set; } = Array.Empty<GetContactDto>();
    }
}
