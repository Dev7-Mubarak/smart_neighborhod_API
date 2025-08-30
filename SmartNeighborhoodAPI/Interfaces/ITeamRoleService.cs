using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface ITeamRoleService
    {
        Task<ApiResponse<TeamRole>> AddAsync(string RoleName);
        Task<ApiResponse<TeamRole>> DeleteAsync(int roleId);
        Task<ApiResponse<IEnumerable<TeamRole>>> GetAllAsync();
        Task<ApiResponse<TeamRole>> GetByIdAsync(int roleId);
        Task<ApiResponse<TeamRole>> UpdateAsync(int id, string newRoleName);
    }
}