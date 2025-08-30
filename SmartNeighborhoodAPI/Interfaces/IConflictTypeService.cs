using SmartNeighborhoodAPI.Helpers.DTOs.ConflictType;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IConflictTypeService
    {
        Task<ApiResponse<AddConflictTypeDto>> AddAsync(AddConflictTypeDto conflictTypeDto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<GetConflictTypeDto>>> GetAll();
        Task<ApiResponse<GetConflictTypeDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, UpdateConflictTypeDto ComplainTypeDto);
    }
}