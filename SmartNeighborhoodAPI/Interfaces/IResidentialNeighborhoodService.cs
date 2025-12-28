using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IResidentialNeighborhoodService
    {
        Task<ApiResponse<ReturnResidentialNeighborhoodDto>> CreateAsync(CreateResidentialNeighborhoodDto dto);
        Task<ApiResponse<List<ReturnResidentialNeighborhoodDto>>> GetAllAsync();
        Task<ApiResponse<ReturnResidentialNeighborhoodDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialNeighborhoodDto dto);
        Task<ApiResponse<string>> DeleteAsync(int id);
    }

}
