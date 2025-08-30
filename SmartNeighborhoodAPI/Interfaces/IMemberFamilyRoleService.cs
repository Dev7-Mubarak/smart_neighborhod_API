using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;

namespace SmartNeighborhoodAPI.Services
{
    public interface IMemberFamilyRoleService
    {
        Task<ApiResponse<MemberFamilyRole>> AddAsync(string nameMemberFamilyRole);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<MemberFamilyRoleDto>>> GetAll();
        Task<ApiResponse<MemberFamilyRoleDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, string newNameMemberFamilyRole);
    }
}