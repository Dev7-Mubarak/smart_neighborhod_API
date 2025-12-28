using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Helpers;

namespace SmartNeighborhoodAPI.Services
{
    public class ResidentialNeighborhoodService : IResidentialNeighborhoodService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ResidentialNeighborhoodService(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> CreateAsync(CreateResidentialNeighborhoodDto dto)
        {
            if (await _context.ResidentialNeighborhoods.AnyAsync(n => n.Name == dto.Name))
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "Neighborhood name already exists");

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == dto.NeighborhoodManagerId);

            if (user == null)
            {
                var person = await _context.People.FindAsync(dto.NeighborhoodManagerId);
                if (person == null)
                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "Person not found");

                user = new AppUser
                {
                    UserName = $"manager{dto.NeighborhoodManagerId}",
                    Email = $"manager{dto.NeighborhoodManagerId}@smartneighborhood.local",
                    PersonId = dto.NeighborhoodManagerId,
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, "Password@123");
                if (!result.Succeeded)
                {
                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            if (!await _userManager.IsInRoleAsync(user, Role.ResidentialNeighborhoodManager))
            {
                await _userManager.AddToRoleAsync(user, Role.ResidentialNeighborhoodManager);
            }

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
        public async Task<ApiResponse<List<ReturnResidentialNeighborhoodDto>>> GetAllAsync(
            CancellationToken ct = default)
        {
            var data = await _context.ResidentialNeighborhoods
                .AsNoTracking()
                .Include(n => n.NeighborhoodManager)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .OrderBy(n => n.Name) 
                .ToListAsync(ct);

            return ApiResponse<List<ReturnResidentialNeighborhoodDto>>
                .Success(data.Select(n => n.ToDto()).ToList());
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

            if (await _context.ResidentialNeighborhoods.AnyAsync(n => n.Name == dto.Name && n.Id != id))
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Neighborhood name already exists");

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return ApiResponse<string>.Success("Neighborhood updated");
        }

        public async Task<ApiResponse<string>> ChangeManagerAsync(int neighborhoodId, int newManagerPersonId)
        {
            var neighborhood = await _context.ResidentialNeighborhoods.FindAsync(neighborhoodId);
            if (neighborhood == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Neighborhood not found");

            // 1. Handle Old Manager
            var oldManagerUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == neighborhood.NeighborhoodManagerId);
            if (oldManagerUser != null)
            {
                // Remove role from old manager
                if (await _userManager.IsInRoleAsync(oldManagerUser, Role.ResidentialNeighborhoodManager))
                {
                    await _userManager.RemoveFromRoleAsync(oldManagerUser, Role.ResidentialNeighborhoodManager);
                }
            }

            // 2. Handle New Manager
            var newManagerUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == newManagerPersonId);
            if (newManagerUser == null)
            {
                var person = await _context.People.FindAsync(newManagerPersonId);
                if (person == null)
                    return ApiResponse<string>.Error(HttpStatusCode.NotFound, "New manager person not found");

                newManagerUser = new AppUser
                {
                    UserName = $"manager{newManagerPersonId}",
                    Email = $"manager{newManagerPersonId}@smartneighborhood.local",
                    PersonId = newManagerPersonId,
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newManagerUser, "Password@123");
                if (!result.Succeeded)
                {
                    return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Failed to create user for new manager: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            if (!await _userManager.IsInRoleAsync(newManagerUser, Role.ResidentialNeighborhoodManager))
            {
                await _userManager.AddToRoleAsync(newManagerUser, Role.ResidentialNeighborhoodManager);
            }

            // 3. Update Neighborhood
            neighborhood.NeighborhoodManagerId = newManagerPersonId;
            await _context.SaveChangesAsync();

            return ApiResponse<string>.Success("Neighborhood manager changed successfully");
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
