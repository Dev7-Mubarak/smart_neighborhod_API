using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IProjectService
    {
        Task<ApiResponse<ReturnProjectDto>> AddAsync(ProjectDto projectDto);
        Task<ApiResponse<string>> AssignFamilyToProjectAsync(int projectId, int familyId);
        Task<ApiResponse<string>> AssignTeamToProjectAsync(int projectId, int teamId);
        Task<ApiResponse<string>> DeleteAssignTeamFromProjectAsync(int projectId, int teamId);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<string>> DeleteFamilyFromProjectAsync(int projectId, int familyId);
        Task<ApiResponse<PaginatedResult<ReturnProjectDto>>> GetAllAsync(ProjectFilterParams filter);
        Task<ApiResponse<ReturnProjectDto>> GetByIdAsync(int id);
        Task<ApiResponse<List<BeneficiaryFamilies>>> GetProjectBlocksWithBeneficiaryFamilies(int projectId);
        Task<ApiResponse<string>> UpdateAsync(int id, ProjectDto projectDto);
    }
}