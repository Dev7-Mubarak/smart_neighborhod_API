using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IConflictCaseService
    {
        Task<ApiResponse<ReturnConflictCaseDto>> AddAsync(AddConflictCaseDto conflictCaseDto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<GetConflictCaseDto>>> GetAll();
        Task<ApiResponse<IEnumerable<GetConflictCaseDto>>> GetByFamilyMemberIdAsync(int familyMemberId);
        Task<ApiResponse<GetConflictCaseDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, UpdateConflictCaseDto conflictCaseDto);
    }
}