using SmartNeighborhoodAPI.Helpers.DTOs.Auth;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IGovernmentInstitutionsService
    {
        Task<ApiResponse<GetGovernmentInstitutionsDto>> AddAuthorityAsync(AddGovernmentInstitutionsDto dto, CancellationToken ct = default);
        Task<ApiResponse<PaginatedResult<GetGovernmentInstitutionsDto>>> GetAllAuthoritiesAsync(int page = 1, int pageSize = 10, CancellationToken ct = default);
        Task<ApiResponse<GetGovernmentInstitutionsDto>> GetAuthorityByIdAsync(int id, CancellationToken ct = default);
        Task<ApiResponse<string>> UpdateAuthorityAsync(int id, UpdateAuthorityDto dto, CancellationToken ct = default);
        Task<ApiResponse<string>> DeleteAuthorityAsync(int id, CancellationToken ct = default);
    }
}
