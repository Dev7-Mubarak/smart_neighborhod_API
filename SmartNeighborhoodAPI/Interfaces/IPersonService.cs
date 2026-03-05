using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace OurProjectSmartNeiborhood.Services
{
    public interface IPersonService
    {
        Task<ApiResponse<Person>> AddAsync(CreatePersonDto dto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<PaginatedResult<PersonDto>>> GetAllAsync(PersonFilterParams filter);
        Task<ApiResponse<PersonDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> UpdateAsync(int id, CreatePersonDto dto);
    }
}