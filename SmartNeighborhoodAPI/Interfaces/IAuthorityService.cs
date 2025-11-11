using SmartNeighborhoodAPI.Helpers.DTOs.Auth;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IAuthorityService
    {
        Task<ApiResponse<GetAuthorityDto>> AddAuthorityAsync(AddAuthorityDto dto, CancellationToken ct = default);
        Task<ApiResponse<PaginatedResult<GetAuthorityDto>>> GetAllAuthoritiesAsync(int page = 1, int pageSize = 10, CancellationToken ct = default);
        Task<ApiResponse<GetAuthorityDto>> GetAuthorityByIdAsync(int id, CancellationToken ct = default);
        Task<ApiResponse<string>> UpdateAuthorityAsync(int id, UpdateAuthorityDto dto, CancellationToken ct = default);
        Task<ApiResponse<string>> DeleteAuthorityAsync(int id, CancellationToken ct = default);
    }
}
