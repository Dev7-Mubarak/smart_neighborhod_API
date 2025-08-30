namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IFamilyTypeService
    {
        Task<ApiResponse<string>> AddAsync(string nameFamilyType);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<GetFamilyTypeDto>>> GetAll();
        Task<ApiResponse<GetFamilyTypeDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, string nameFamilyTypeDto);
    }
}