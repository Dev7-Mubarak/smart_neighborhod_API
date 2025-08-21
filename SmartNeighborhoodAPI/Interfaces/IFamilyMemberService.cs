using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IFamilyMemberService
    {
        Task<ApiResponse<FamilyMember>> AddAsync(FamilyMemberDto familyMemberDto);
        Task<ApiResponse<string>> DeleteAsync(int id, int familyId);
        Task<ApiResponse<IEnumerable<ReturnFamilyMemberWithFullInfo>>> GetAllAsync();
        Task<ApiResponse<ReturnFamilyMemberWithFullInfo>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, FamilyMember FamilyMember);
    }
}