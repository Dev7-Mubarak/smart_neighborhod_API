
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;


namespace SmartNeighborhoodAPI.Services
{

    public class PassportApplicationService : IPassportApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PassportApplicationService> _logger;

        public PassportApplicationService(ApplicationDbContext context, ILogger<PassportApplicationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<GetPassportApplicationDto>> AddAsync(AddPassportApplicationDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("Adding PassportApplication {ApplicationNumber}", dto.ApplicationNumber);

            var entity = new PassportApplication
            {
                ResidentId = dto.ResidentId,
                ApplicationNumber = dto.ApplicationNumber,
                Notes = dto.Notes,
                Status = ApplicationStatus.Received,
                LastUpdate = DateTime.UtcNow
            };

            _context.PassportApplications.Add(entity);
            await _context.SaveChangesAsync(ct);

            var result = GetPassportApplicationDto.Create(entity);
            return ApiResponse<GetPassportApplicationDto>.Success(result, "");
        }

        public async Task<ApiResponse<PaginatedResult<GetAllPassportApplicationDto>>> GetAllAsync(
            string? applicationNumber = null,
            ApplicationStatus? status = null,
            int page = 1,
            int pageSize = 10,
            CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching PassportApplications - filter applicationNumber:{AppNum} status:{Status}", applicationNumber, status);

            var query = _context.PassportApplications
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(applicationNumber))
                query = query.Where(p => p.ApplicationNumber!.Contains(applicationNumber));

            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

            var totalCount = await query.CountAsync(ct);

            if (totalCount == 0)
            {
                return ApiResponse<PaginatedResult<GetAllPassportApplicationDto>>.Success(
                    PaginatedResult<GetAllPassportApplicationDto>.Success(new List<GetAllPassportApplicationDto>(), 0, page, pageSize),
                    "No passport applications found");
            }

            var data = await query
                .OrderBy(p => p.ApplicationNumber)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(GetAllPassportApplicationDto.Selector)
                .ToListAsync(ct);

            var paged = PaginatedResult<GetAllPassportApplicationDto>.Success(data, totalCount, page, pageSize);
            return ApiResponse<PaginatedResult<GetAllPassportApplicationDto>>.Success(paged);
        }

        public async Task<ApiResponse<GetPassportApplicationDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _context.PassportApplications.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);
            if (entity == null)
                return ApiResponse<GetPassportApplicationDto>.Error(HttpStatusCode.NotFound, "Passport application not found");

            var dto = GetPassportApplicationDto.Create(entity);
            return ApiResponse<GetPassportApplicationDto>.Success(dto);
        }

  

        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdatePassportApplicationDto dto, CancellationToken ct = default)
        {
            var entity = await _context.PassportApplications.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Passport application not found");

            entity.ApplicationNumber = dto.ApplicationNumber;
            entity.Status = dto.Status;
            entity.Notes = dto.Notes;
            entity.LastUpdate = DateTime.UtcNow;

            _context.PassportApplications.Update(entity);
            await _context.SaveChangesAsync(ct);

            return ApiResponse<string>.Success("Passport application updated successfully");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _context.PassportApplications.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Passport application not found");

            _context.PassportApplications.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return ApiResponse<string>.Success("Passport application deleted successfully");
        }
    }

}
