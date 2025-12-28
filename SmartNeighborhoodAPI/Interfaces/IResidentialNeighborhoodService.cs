using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IResidentialNeighborhoodService
    {
        Task<ApiResponse<ReturnResidentialNeighborhoodDto>> CreateAsync(CreateResidentialNeighborhoodDto dto);
        Task<ApiResponse<PaginatedResult<ReturnResidentialNeighborhoodDto>>> GetAllAsync(
            string? name,
            string? managerId,
            int page,
            int pageSize,
            CancellationToken ct = default);
        Task<ApiResponse<ReturnResidentialNeighborhoodDto>> GetByIdAsync(int id);
        Task<ApiResponse<PaginatedResult<ResidentialSearchResultDto>>> SearchAsync(
            string keyword,
            int page,
            int pageSize,
            CancellationToken ct = default);
        Task<ApiResponse<ResidentialDashboardDto>> GetDashboardAsync(
        CancellationToken ct = default);
        Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialNeighborhoodDto dto);
        Task<ApiResponse<string>> DeleteAsync(int id);
    }

}
