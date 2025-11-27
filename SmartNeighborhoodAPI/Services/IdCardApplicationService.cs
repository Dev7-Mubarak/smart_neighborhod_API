
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using System.Net;

public class IdCardApplicationService : IIdCardApplicationService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<IdCardApplicationService> _logger;

    public IdCardApplicationService(ApplicationDbContext context, ILogger<IdCardApplicationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApiResponse<GetIdCardApplicationDto>> AddAsync(AddIdCardApplicationDto dto, CancellationToken ct = default)
    {
        _logger.LogInformation("Adding IDCardApplication {ApplicationNumber}", dto.ApplicationNumber);

        var entity = new IDCardApplication
        {
            ResidentId = dto.ResidentId,
            ApplicationNumber = dto.ApplicationNumber,
            Notes = dto.Notes,
            Status = ApplicationStatus.Received,
            LastUpdate = DateTime.UtcNow
        };

        _context.Set<IDCardApplication>().Add(entity);
        await _context.SaveChangesAsync(ct);

        return ApiResponse<GetIdCardApplicationDto>.Success(GetIdCardApplicationDto.Create(entity));
    }

    public async Task<ApiResponse<PaginatedResult<GetAllIdCardApplicationDto>>> GetAllAsync(
        string? applicationNumber = null,
        ApplicationStatus? status = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        _logger.LogInformation("Fetching IDCardApplications - filter applicationNumber:{AppNum} status:{Status}", applicationNumber, status);

        var query = _context.Set<IDCardApplication>()
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(applicationNumber))
            query = query.Where(p => p.ApplicationNumber!.Contains(applicationNumber));

        if (status.HasValue)
            query = query.Where(p => p.Status == status.Value);

        var totalCount = await query.CountAsync(ct);

        if (totalCount == 0)
        {
            return ApiResponse<PaginatedResult<GetAllIdCardApplicationDto>>.Success(
                PaginatedResult<GetAllIdCardApplicationDto>.Success(new List<GetAllIdCardApplicationDto>(), 0, page, pageSize),
                "No ID card applications found");
        }

        var data = await query
            .OrderBy(p => p.ApplicationNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(GetAllIdCardApplicationDto.Selector)
            .ToListAsync(ct);

        var paged = PaginatedResult<GetAllIdCardApplicationDto>.Success(data, totalCount, page, pageSize);
        return ApiResponse<PaginatedResult<GetAllIdCardApplicationDto>>.Success(paged);
    }

    public async Task<ApiResponse<GetIdCardApplicationDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _context.Set<IDCardApplication>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity == null)
            return ApiResponse<GetIdCardApplicationDto>.Error(HttpStatusCode.NotFound, "ID card application not found");

        return ApiResponse<GetIdCardApplicationDto>.Success(GetIdCardApplicationDto.Create(entity));
    }


    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateIdCardApplicationDto dto, CancellationToken ct = default)
    {
        var entity = await _context.Set<IDCardApplication>().FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity == null)
            return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ID card application not found");

        entity.ApplicationNumber = dto.ApplicationNumber;
        entity.Status = dto.Status;
        entity.Notes = dto.Notes;
        entity.LastUpdate = DateTime.UtcNow;

        _context.Set<IDCardApplication>().Update(entity);
        await _context.SaveChangesAsync(ct);

        return ApiResponse<string>.Success("ID card application updated successfully");
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _context.Set<IDCardApplication>().FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity == null)
            return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ID card application not found");

        _context.Set<IDCardApplication>().Remove(entity);
        await _context.SaveChangesAsync(ct);

        return ApiResponse<string>.Success("ID card application deleted successfully");
    }
}
