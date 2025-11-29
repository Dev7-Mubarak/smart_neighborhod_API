
using SmartNeighborhoodAPI.Entites.Enums;

public interface IMedicineService
{
    Task<ApiResponse<GetMedicineDto>> AddAsync(AddMedicineDto dto, CancellationToken ct = default);

    Task<ApiResponse<PaginatedResult<GetAllMedicineDto>>> GetAllAsync(
        string? name = null,
        MedicineType? type = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);

    Task<ApiResponse<GetMedicineDto>> GetByIdAsync(int id, CancellationToken ct = default);

    Task<ApiResponse<string>> UpdateAsync(int id, UpdateMedicineDto dto, CancellationToken ct = default);

    Task<ApiResponse<string>> DeleteAsync(int id, CancellationToken ct = default);

    Task<ApiResponse<GetMedicineBatchDto>> AddBatchAsync(AddMedicineBatchDto dto, CancellationToken ct = default);

    Task<ApiResponse<List<GetMedicineBatchDto>>> GetBatchesByMedicineAsync(int medicineId, CancellationToken ct = default);

    Task<ApiResponse<GetMedicineBatchDto>> UpdateBatchAsync(int batchId, UpdateMedicineBatchDto dto, CancellationToken ct = default);

    Task<ApiResponse<string>> DeleteBatchAsync(int batchId, CancellationToken ct = default);
}
