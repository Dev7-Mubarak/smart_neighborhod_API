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
    }

}
