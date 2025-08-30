using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface ITeamMemberService
    {
        Task<ApiResponse<string>> AddAsync(AddTeamMemberDto dto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<TeamMemberDetailsDto>>> GetAll();
        Task<ApiResponse<TeamMemberDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, UpdateTeamMemberDto dto);
    }
}