
using SmartNeighborhoodAPI.Entites.Enums;
namespace SmartNeighborhoodAPI.Interfaces
{


    public interface IPassportApplicationService
    {
        Task<ApiResponse<GetPassportApplicationDto>> AddAsync(AddPassportApplicationDto dto, CancellationToken ct = default);

        Task<ApiResponse<PaginatedResult<GetAllPassportApplicationDto>>> GetAllAsync(
            string? applicationNumber = null,
            ApplicationStatus? status = null,
            int page = 1,
            int pageSize = 10,
            CancellationToken ct = default);

        Task<ApiResponse<GetPassportApplicationDto>> GetByIdAsync(int id, CancellationToken ct = default);



        Task<ApiResponse<string>> UpdateAsync(int id, UpdatePassportApplicationDto dto, CancellationToken ct = default);

        Task<ApiResponse<string>> DeleteAsync(int id, CancellationToken ct = default);
    }

}
