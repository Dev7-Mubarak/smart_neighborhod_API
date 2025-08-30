
namespace SmartNeighborhoodAPI.Services
{
    public interface IFamilyCatgoryService
    {
        Task<ApiResponse<string>> AddAsync(string name);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<FamilyCatgoryDto>>> GetAll();
        Task<ApiResponse<FamilyCatgoryDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, string nameFamilyCatgory);
    }
}