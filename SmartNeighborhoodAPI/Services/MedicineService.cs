
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using System.Net;

public class MedicineService : IMedicineService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MedicineService> _logger;

    public MedicineService(ApplicationDbContext context, ILogger<MedicineService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApiResponse<GetMedicineDto>> AddAsync(AddMedicineDto dto, CancellationToken ct = default)
    {
        _logger.LogInformation("Adding medicine {Name}", dto.Name);

        var entity = new Medicine
        {
            Name = dto.Name,
            Type = dto.Type,
            Description = dto.Description
        };

        _context.Medicines.Add(entity);
        await _context.SaveChangesAsync(ct);

        return ApiResponse<GetMedicineDto>.Success(GetMedicineDto.Create(entity));
    }

    public async Task<ApiResponse<PaginatedResult<GetAllMedicineDto>>> GetAllAsync(string? name = null, MedicineType? type = null, int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        _logger.LogInformation("Fetching medicines filter name:{Name} type:{Type}", name, type);

        var query = _context.Medicines.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(m => m.Name!.Contains(name));

        if (type.HasValue)
            query = query.Where(m => m.Type == type.Value);

        var total = await query.CountAsync(ct);
        if (total == 0)
            return ApiResponse<PaginatedResult<GetAllMedicineDto>>.Success(PaginatedResult<GetAllMedicineDto>.Success(new List<GetAllMedicineDto>(), 0, page, pageSize), "No medicines");

        var data = await query
            .OrderBy(m => m.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(GetAllMedicineDto.Selector)
            .ToListAsync(ct);

        return ApiResponse<PaginatedResult<GetAllMedicineDto>>.Success(PaginatedResult<GetAllMedicineDto>.Success(data, total, page, pageSize));
    }

    public async Task<ApiResponse<GetMedicineDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _context.Medicines.AsNoTracking().Include(m => m.Batches).FirstOrDefaultAsync(m => m.Id == id, ct);
        if (entity == null)
            return ApiResponse<GetMedicineDto>.Error(HttpStatusCode.NotFound, "Medicine not found");

        var dto = GetMedicineDto.Create(entity);
        return ApiResponse<GetMedicineDto>.Success(dto);
    }

    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateMedicineDto dto, CancellationToken ct = default)
    {
        var entity = await _context.Medicines.FirstOrDefaultAsync(m => m.Id == id, ct);
        if (entity == null)
            return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Medicine not found");

        entity.Name = dto.Name;
        entity.Type = dto.Type;
        entity.Description = dto.Description;

        _context.Medicines.Update(entity);
        await _context.SaveChangesAsync(ct);

        return ApiResponse<string>.Success("Medicine updated");
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _context.Medicines.Include(m => m.Batches).FirstOrDefaultAsync(m => m.Id == id, ct);
        if (entity == null)
            return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Medicine not found");

        if (entity.Batches != null && entity.Batches.Any())
        {
            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Cannot delete medicine with existing batches");
        }

        _context.Medicines.Remove(entity);
        await _context.SaveChangesAsync(ct);
        return ApiResponse<string>.Success("Medicine deleted");
    }
    public async Task<ApiResponse<GetMedicineBatchDto>> AddBatchAsync(AddMedicineBatchDto dto, CancellationToken ct = default)
    {
        var medicine = await _context.Medicines.FirstOrDefaultAsync(m => m.Id == dto.MedicineId, ct);
        if (medicine == null)
            return ApiResponse<GetMedicineBatchDto>.Error(HttpStatusCode.NotFound, "Medicine not found");

        var batch = new MedicineBatch
        {
            MedicineId = dto.MedicineId,
            Quantity = dto.Quantity,
            ArrivalDate = dto.ArrivalDate,
            ExpireDate = dto.ExpireDate,
            Provider = dto.Provider
        };

        _context.MedicineBatches.Add(batch);
        await _context.SaveChangesAsync(ct);

        return ApiResponse<GetMedicineBatchDto>.Success(GetMedicineBatchDto.Create(batch));
    }

    public async Task<ApiResponse<List<GetMedicineBatchDto>>> GetBatchesByMedicineAsync(int medicineId, CancellationToken ct = default)
    {
        var batches = await _context.MedicineBatches.AsNoTracking()
            .Where(b => b.MedicineId == medicineId)
            .OrderBy(b => b.ExpireDate)
            .Select(b => GetMedicineBatchDto.Create(b))
            .ToListAsync(ct);

        return ApiResponse<List<GetMedicineBatchDto>>.Success(batches);
    }

    public async Task<ApiResponse<GetMedicineBatchDto>> UpdateBatchAsync(int batchId, UpdateMedicineBatchDto dto, CancellationToken ct = default)
    {
        var batch = await _context.MedicineBatches.FirstOrDefaultAsync(b => b.Id == batchId, ct);
        if (batch == null)
            return ApiResponse<GetMedicineBatchDto>.Error(HttpStatusCode.NotFound, "Batch not found");

        batch.Quantity = dto.Quantity;
        batch.ArrivalDate = dto.ArrivalDate;
        batch.ExpireDate = dto.ExpireDate;
        batch.Provider = dto.Provider;

        _context.MedicineBatches.Update(batch);
        await _context.SaveChangesAsync(ct);

        return ApiResponse<GetMedicineBatchDto>.Success(GetMedicineBatchDto.Create(batch));
    }

    public async Task<ApiResponse<string>> DeleteBatchAsync(int batchId, CancellationToken ct = default)
    {
        var batch = await _context.MedicineBatches.FirstOrDefaultAsync(b => b.Id == batchId, ct);
        if (batch == null)
            return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Batch not found");

        _context.MedicineBatches.Remove(batch);
        await _context.SaveChangesAsync(ct);

        return ApiResponse<string>.Success("Batch deleted");
    }
}
