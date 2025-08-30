using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Helpers.DTOs.Teams;

namespace SmartNeighborhoodAPI.Services
{
    public interface ITeamsService
    {
        Task<ApiResponse<TeamDto>> AddAsync(TeamDto dto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<CustomTeamDto>>> GetAll();
        Task<ApiResponse<TeamDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<ReturnProjectDto>>> GetTeamProjects(int teamId);
        Task<ApiResponse<TeamDto>> UpdateAsync(int teamId, TeamDto dto);
    }
}