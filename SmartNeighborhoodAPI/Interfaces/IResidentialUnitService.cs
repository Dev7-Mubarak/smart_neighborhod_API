
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits;

public interface IResidentialUnitService
{
    Task<ApiResponse<ReturnResidentialUnitDto>> AddAsync(AddResidentialUnitDto dto, CancellationToken ct = default);

    Task<ApiResponse<PaginatedResult<ReturnResidentialUnitDto>>> GetAllAsync(
        string? name = null,
        string? managerId = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);

    Task<ApiResponse<ReturnResidentialUnitDto>> GetByIdAsync(int id, CancellationToken ct = default);

    Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialUnitDto dto, CancellationToken ct = default);

    Task<ApiResponse<string>> DeleteAsync(int id, CancellationToken ct = default);
}

