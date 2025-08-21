using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace OurProjectSmartNeiborhood.Services
{
    public interface IPersonService
    {
        Task<ApiResponse<Person>> AddAsync(CreatePersonDto dto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<PaginatedResult<PersonDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? search = null);
        Task<ApiResponse<PersonDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, CreatePersonDto dto);
    }
}