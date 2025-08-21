namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IProjectFamilieservice
    {
        Task<ApiResponse<ProjectFamilyDto>> AddAsync(ProjectFamilyDto ProjectFamilyDto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<ProjectFamilyDto>>> GetAll();
        Task<ApiResponse<ProjectFamilyDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, ProjectFamilyDto ProjectFamilyDto);
    }
}