using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ResidentialNeighborhoodService : IResidentialNeighborhoodService
    {
        private readonly ApplicationDbContext _context;

        public ResidentialNeighborhoodService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> CreateAsync(CreateResidentialNeighborhoodDto dto)
        {
            var entity = new ResidentialNeighborhood
            {
                Name = dto.Name,
                NeighborhoodManagerId = dto.NeighborhoodManagerId
            };

            _context.ResidentialNeighborhoods.Add(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(e => e.NeighborhoodManager).LoadAsync();

            return ApiResponse<ReturnResidentialNeighborhoodDto>.Success(entity.ToDto());
        }

        public async Task<ApiResponse<PaginatedResult<ReturnResidentialNeighborhoodDto>>> GetAllAsync(
            string? name,
            string? managerId,
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var query = _context.ResidentialNeighborhoods
                .AsNoTracking()
                .Include(n => n.NeighborhoodManager)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.UnitManager)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(n => n.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(managerId))
                query = query.Where(n => n.NeighborhoodManagerId == managerId);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(n => n.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var dtoList = items.Select(n => n.ToDto()).ToList();

            return ApiResponse<PaginatedResult<ReturnResidentialNeighborhoodDto>>.Success(
                PaginatedResult<ReturnResidentialNeighborhoodDto>.Success(dtoList, total, page, pageSize)
            );
        }

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> GetByIdAsync(int id)
        {
            var entity = await _context.ResidentialNeighborhoods
                .Include(n => n.NeighborhoodManager)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (entity == null)
                return ApiResponse<ReturnResidentialNeighborhoodDto>
                    .Error(HttpStatusCode.NotFound, "Neighborhood not found");

            return ApiResponse<ReturnResidentialNeighborhoodDto>.Success(entity.ToDto());
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialNeighborhoodDto dto)
        {
            var entity = await _context.ResidentialNeighborhoods.FindAsync(id);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Neighborhood not found");

            entity.Name = dto.Name;
            entity.NeighborhoodManagerId = dto.NeighborhoodManagerId;

            await _context.SaveChangesAsync();
            return ApiResponse<string>.Success("Neighborhood updated");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.ResidentialNeighborhoods
                .Include(n => n.ResidentialUnits)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Neighborhood not found");

            if (entity.ResidentialUnits.Any())
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest,
                    "Cannot delete neighborhood with units");

            _context.ResidentialNeighborhoods.Remove(entity);
            await _context.SaveChangesAsync();

            return ApiResponse<string>.Success("Neighborhood deleted");
        }
        public async Task<ApiResponse<PaginatedResult<ResidentialSearchResultDto>>> SearchAsync(
            string keyword,
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return ApiResponse<PaginatedResult<ResidentialSearchResultDto>>
                    .Error(HttpStatusCode.BadRequest, "Search keyword is required");

            keyword = keyword.Trim();

            var query =
                from n in _context.ResidentialNeighborhoods.AsNoTracking()
                from u in n.ResidentialUnits.DefaultIfEmpty()
                from b in u.Blocks.DefaultIfEmpty()
                where
                    n.Name.Contains(keyword) ||
                    (u != null && u.Name.Contains(keyword)) ||
                    (b != null && b.Name.Contains(keyword))
                select new ResidentialSearchResultDto
                {
                    NeighborhoodId = n.Id,
                    NeighborhoodName = n.Name,
                    UnitId = u != null ? u.Id : null,
                    UnitName = u != null ? u.Name : null,
                    BlockId = b != null ? b.Id : null,
                    BlockName = b != null ? b.Name : null
                };

            var total = await query.CountAsync(ct);

            var items = await query
                .Distinct()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return ApiResponse<PaginatedResult<ResidentialSearchResultDto>>.Success(
                PaginatedResult<ResidentialSearchResultDto>.Success(items, total, page, pageSize));
        }

        public async Task<ApiResponse<ResidentialDashboardDto>> GetDashboardAsync(
        CancellationToken ct = default)
        {
            var neighborhoods = await _context.ResidentialNeighborhoods
                .AsNoTracking()
                .Select(n => new
                {
                    n.Id,
                    n.Name,
                    UnitsCount = n.ResidentialUnits.Count,
                    BlocksCount = n.ResidentialUnits.Sum(u => u.Blocks.Count)
                })
                .ToListAsync(ct);

            var dashboard = new ResidentialDashboardDto
            {
                TotalNeighborhoods = neighborhoods.Count,
                TotalUnits = neighborhoods.Sum(n => n.UnitsCount),
                TotalBlocks = neighborhoods.Sum(n => n.BlocksCount),
                Neighborhoods = neighborhoods.Select(n => new NeighborhoodStatsDto
                {
                    NeighborhoodId = n.Id,
                    NeighborhoodName = n.Name,
                    UnitsCount = n.UnitsCount,
                    BlocksCount = n.BlocksCount
                }).ToList()
            };

            return ApiResponse<ResidentialDashboardDto>.Success(dashboard);
        }


    }

}
