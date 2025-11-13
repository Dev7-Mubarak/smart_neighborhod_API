using SmartNeighborhoodAPI.Helpers.DTOs.Government_InstitutionsContacts;

namespace MramProject.Interface
{
    public interface IGovernmentInstitutionContactService
    {
        Task<ApiResponse<GetGovernmentInstitutionContactDto>> AddContactAsync(int authorityId, AddGovernmentInstitutionContactDto dto, CancellationToken ct = default);
        Task<ApiResponse<IEnumerable<GetGovernmentInstitutionContactDto>>> GetContactsByAuthorityAsync(int authorityId, CancellationToken ct = default);
        Task<ApiResponse<GetGovernmentInstitutionContactDto>> GetContactByIdAsync(int id, CancellationToken ct = default);
        Task<ApiResponse<string>> UpdateContactAsync(int id, UpdateGovernmentInstitutionContactDto dto, CancellationToken ct = default);
        Task<ApiResponse<string>> DeleteContactAsync(int id, CancellationToken ct = default);
    }
}
