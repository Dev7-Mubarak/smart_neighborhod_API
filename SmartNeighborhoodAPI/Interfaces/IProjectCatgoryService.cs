namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IProjectCatgoryService
    {
        Task<ApiResponse<ProjectCatgoryDto>> AddAsync(ProjectCatgoryDto ProjectCatgoryDto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<ProjectCatogory>>> GetAll();
        Task<ApiResponse<ProjectCatgoryDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, ProjectCatgoryDto ProjectCatgoryDto);
    }
}