
using SmartNeighborhoodAPI.Entites.Enums;

public interface IIdCardApplicationService
{
    Task<ApiResponse<GetIdCardApplicationDto>> AddAsync(AddIdCardApplicationDto dto, CancellationToken ct = default);

    Task<ApiResponse<PaginatedResult<GetAllIdCardApplicationDto>>> GetAllAsync(
        string? applicationNumber = null,
        ApplicationStatus? status = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);

    Task<ApiResponse<GetIdCardApplicationDto>> GetByIdAsync(int id, CancellationToken ct = default);

    Task<ApiResponse<string>> UpdateAsync(int id, UpdateIdCardApplicationDto dto, CancellationToken ct = default);

    Task<ApiResponse<string>> DeleteAsync(int id, CancellationToken ct = default);
}
