using SmartNeighborhoodAPI.Helpers.DTOs.Contact;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IContactService
    {
        Task<ApiResponse<GetContactDto>> AddContactAsync(int authorityId, AddContactDto dto, CancellationToken ct = default);
        Task<ApiResponse<IEnumerable<GetContactDto>>> GetContactsByAuthorityAsync(int authorityId, CancellationToken ct = default);
        Task<ApiResponse<GetContactDto>> GetContactByIdAsync(int id, CancellationToken ct = default);
        Task<ApiResponse<string>> UpdateContactAsync(int id, UpdateContactDto dto, CancellationToken ct = default);
        Task<ApiResponse<string>> DeleteContactAsync(int id, CancellationToken ct = default);
    }
}
