using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using Microsoft.EntityFrameworkCore;

namespace SmartNeighborhoodAPI.Services
{
    public class ResidentialNeighborhoodService : IResidentialNeighborhoodService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IManagerAccountService _managerAccountService;
        private readonly ILogger<ResidentialNeighborhoodService> _logger;

        public ResidentialNeighborhoodService(ApplicationDbContext context, UserManager<AppUser> userManager, IManagerAccountService managerAccountService, ILogger<ResidentialNeighborhoodService> logger)
        {
            _context = context;
            _userManager = userManager;
            _managerAccountService = managerAccountService;
            _logger = logger;
        }

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> CreateAsync(CreateResidentialNeighborhoodDto dto)
        {
            _logger.LogInformation("Attempting to create a new residential neighborhood with name: {Name}", dto.Name);

            if (await _context.ResidentialNeighborhoods.AnyAsync(n => n.Name == dto.Name))
            {
                _logger.LogWarning("Neighborhood with name '{Name}' already exists", dto.Name);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "اسم الحي السكني موجود بالفعل");
            }

            var person = await _context.People.FindAsync(dto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", dto.PersonId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Use ManagerAccountService to create manager
                    var managerResult = await _managerAccountService.CreateManagerAccountAsync(
                        dto.PersonId, 
                        dto.Identifier, 
                        dto.Password, 
                        Role.ResidentialNeighborhoodManager
                    );

                    if (!managerResult.IsSuccess)
                    {
                        return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(managerResult.StatusCode, managerResult.Message, managerResult.Errors);
                    }

                    var user = managerResult.Data;
                    var entity = new ResidentialNeighborhood
                    {
                        Name = dto.Name,
                        NeighborhoodManagerId = user.Id 
                    };

                    _context.ResidentialNeighborhoods.Add(entity);
                    await _context.SaveChangesAsync();

                    await _context.Entry(entity).Reference(e => e.NeighborhoodManager).LoadAsync();
                    if (entity.NeighborhoodManager != null)
                    {
                        await _context.Entry(entity.NeighborhoodManager).Reference(nm => nm.Person).LoadAsync();
                    }

                    await transaction.CommitAsync();

                    _logger.LogInformation("Successfully created residential neighborhood '{Name}' with ID {Id}", entity.Name, entity.Id);

                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Success(entity.ToDto(), "تم إنشاء الحي السكني بنجاح");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in CreateAsync");
                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء إنشاء الحي السكني");
                }
            });
        }
        public async Task<ApiResponse<List<ReturnResidentialNeighborhoodDto>>> GetAllAsync(
            CancellationToken ct = default)
        {
            var data = await _context.ResidentialNeighborhoods
                .AsNoTracking()
                .Include(n => n.NeighborhoodManager)
                    .ThenInclude(nm => nm.Person)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .OrderBy(n => n.Name) 
                .ToListAsync(ct);

            return ApiResponse<List<ReturnResidentialNeighborhoodDto>>
                .Success(data.Select(n => n.ToDto()).ToList(), "تم جلب الأحياء السكنية بنجاح");
        }

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> GetByIdAsync(int id)
        {
            var entity = await _context.ResidentialNeighborhoods
                .Include(n => n.NeighborhoodManager)
                    .ThenInclude(nm => nm.Person)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (entity == null)
                return ApiResponse<ReturnResidentialNeighborhoodDto>
                    .Error(HttpStatusCode.NotFound, "الحي السكني غير موجود");

            return ApiResponse<ReturnResidentialNeighborhoodDto>.Success(entity.ToDto(), "تم جلب الحي السكني بنجاح");
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialNeighborhoodDto dto)
        {
            var entity = await _context.ResidentialNeighborhoods.FindAsync(id);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الحي السكني غير موجود");

            if (await _context.ResidentialNeighborhoods.AnyAsync(n => n.Name == dto.Name && n.Id != id))
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "اسم الحي السكني موجود بالفعل");

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return ApiResponse<string>.Success("تم تحديث الحي السكني بنجاح");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.ResidentialNeighborhoods
                .Include(n => n.ResidentialUnits)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الحي السكني غير موجود");

            if (entity.ResidentialUnits.Any())
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest,
                    "لا يمكن حذف الحي السكني لوجود وحدات سكنية مرتبطة به");

            var managerId = entity.NeighborhoodManagerId;
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Step 6: Delete old manager account (if any)
                    if (managerId != null)
                    {
                        var deleteResult = await _managerAccountService.DeleteManagerAccountAsync(managerId);
                        if (!deleteResult.IsSuccess)
                        {
                            throw new Exception("فشل حذف المدير القديم");
                        }
                    }

                    _context.ResidentialNeighborhoods.Remove(entity);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return ApiResponse<string>.Success("تم حذف الحي السكني بنجاح");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in DeleteAsync");
                    return ApiResponse<string>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء حذف الحي السكني");
                }
            });
        }

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> ChangeManagerAsync(ChangeResidentialManagerDto dto)
        {
            _logger.LogInformation("Initiating change of residential neighborhood manager for NeighborhoodId: {NeighborhoodId}, PersonId: {PersonId}",
                dto.neighborhoodId, dto.PersonId);

            // Step 1: Validate neighborhood
            var neighborhood = await _context.ResidentialNeighborhoods.FindAsync(dto.neighborhoodId);
            if (neighborhood == null)
            {
                _logger.LogWarning("Neighborhood with ID '{NeighborhoodId}' not found.", dto.neighborhoodId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "الحي السكني غير موجود");
            }

            // Step 2: Validate person
            var person = await _context.People.FindAsync(dto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", dto.PersonId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Step 4: Create new manager account
                    var managerResult = await _managerAccountService.CreateManagerAccountAsync(
                         dto.PersonId,
                         dto.Email,
                         dto.Password,
                         Role.ResidentialNeighborhoodManager
                     );

                    if (!managerResult.IsSuccess)
                    {
                        return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(managerResult.StatusCode, managerResult.Message, managerResult.Errors);
                    }
                    
                    var user = managerResult.Data;
                    var oldManagerId = neighborhood.NeighborhoodManagerId;

                    // Step 5: Update neighborhood manager
                    neighborhood.NeighborhoodManagerId = user.Id;
                    await _context.SaveChangesAsync();

                    // Step 6: Delete old manager account (if any)
                    if (oldManagerId != null)
                    {
                         var deleteResult = await _managerAccountService.DeleteManagerAccountAsync(oldManagerId);
                         if (!deleteResult.IsSuccess)
                         {
                             // Consider manual cleanup or specialized exception handling if this fails but new manager is set?
                             // Currently relying on transaction rollback.
                             throw new Exception("فشل حذف المدير القديم");
                         }
                    }

                    await _context.Entry(neighborhood).Reference(e => e.NeighborhoodManager).LoadAsync();
                    if (neighborhood.NeighborhoodManager != null)
                    {
                        await _context.Entry(neighborhood.NeighborhoodManager).Reference(nm => nm.Person).LoadAsync();
                    }

                    await transaction.CommitAsync();

                    _logger.LogInformation("Neighborhood manager updated successfully for neighborhood '{Name}' (ID: {Id})",
                        neighborhood.Name, neighborhood.Id);

                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Success(neighborhood.ToDto(),
                        "تم تغيير مدير الحي السكني بنجاح. تم إرسال بيانات الدخول عبر البريد الإلكتروني.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in ChangeManagerAsync");
                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء معالجة الطلب.");
                }
            });
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
