using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits;
using SmartNeighborhoodAPI.Interfaces;
using System.Data;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ResidentialUnitService
    {
        private readonly ApplicationDbContext _context;
        private readonly IManagerAccountService _managerAccountService;
        private readonly ILogger<ResidentialUnit> _logger;
        private readonly UserManager<AppUser> _userManager;


        public ResidentialUnitService(ApplicationDbContext context, IManagerAccountService managerAccountService, ILogger<ResidentialUnit> logger, UserManager<AppUser> userManager)
        {
            _context = context;
            _managerAccountService = managerAccountService;
            _userManager = userManager;
            _logger = logger;
        }



        public async Task<ApiResponse<IEnumerable<ReturnResidentialUnitDto>>> GetAllAsync(
            CancellationToken ct = default)
        {
            var data = await _context.ResidentialUnits
                .AsNoTracking()
                .Include(u => u.UnitManager)
                    .ThenInclude(um => um.Person)
                .Include(u => u.Blocks)
                    .ThenInclude(b => b.BlockManager)
                .OrderBy(u => u.Name)
                .ToListAsync(ct);

            return ApiResponse<IEnumerable<ReturnResidentialUnitDto>>
                .Success(data.Select(u => u.ToDto()).ToList(), "تم جلب الوحدات السكنية بنجاح");
        }

        public async Task<ApiResponse<ReturnResidentialUnitDto>> AddAsync(AddResidentialUnitDto unitDto)
        {
            _logger.LogInformation("Attempting to add a new residential unit with name: {UnitName}", unitDto.Name);

            // 1. Validation
            var existUnit = await _context.ResidentialUnits.FirstOrDefaultAsync(x => x.Name == unitDto.Name);
            if (existUnit != null)
            {
                _logger.LogWarning("Residential Unit with name '{UnitName}' already exists", unitDto.Name);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.Conflict, "اسم الوحدة السكنية موجود مسبقًا.");
            }

            var neighborHood = await _context.ResidentialNeighborhoods.FindAsync(unitDto.ResidentialNeighborhoodId);
            if (neighborHood == null)
            {
                 _logger.LogWarning("Neighborhood with ID {Id} not found", unitDto.ResidentialNeighborhoodId);
                 return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.NotFound, "الحي السكني غير موجود.");
            }

            var person = await _context.People.FindAsync(unitDto.UnitManagerId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", unitDto.UnitManagerId);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود.");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // 2. Create Manager Account
                    var managerResult = await _managerAccountService.CreateManagerAccountAsync(
                        unitDto.UnitManagerId,
                        unitDto.Email,
                        unitDto.Password,
                        Role.UnitManager
                    );

                    if (!managerResult.IsSuccess)
                    {
                        return ApiResponse<ReturnResidentialUnitDto>.Error(managerResult.StatusCode, managerResult.Message, managerResult.Errors);
                    }

                    var user = managerResult.Data;

                    // 3. Create Unit
                    var unit = new ResidentialUnit
                    {
                        Name = unitDto.Name,
                        UnitManagerId = user.Id,
                        ResidentialNeighborhoodId = unitDto.ResidentialNeighborhoodId
                    };

                    await _context.ResidentialUnits.AddAsync(unit);
                    await _context.SaveChangesAsync();

                    await _context.Entry(unit).Reference(u => u.UnitManager).LoadAsync();
                    if (unit.UnitManager != null)
                    {
                        await _context.Entry(unit.UnitManager).Reference(um => um.Person).LoadAsync();
                    }

                    await transaction.CommitAsync();

                    _logger.LogInformation("Successfully created residential unit '{Name}' with ID {Id}", unit.Name, unit.Id);

                    return ApiResponse<ReturnResidentialUnitDto>.Success(unit.ToDto(), "تم إنشاء الوحدة السكنية بنجاح.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in AddAsync");
                    
                    // Manually attempt cleanup if transaction fails (though rollback should handle DB part, user part is external)
                    // If user creation happened successfully but transaction failed, the user might still exist in Identity if it was committed?
                    // Identity usually has its own context. If share same context/transaction ok.
                    // Assuming different context or no transaction sharing:
                   
                    return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء إنشاء الوحدة السكنية.");
                }
            });
        }

        public async Task<ApiResponse<ReturnResidentialUnitDto>> GetByIdAsync(int id)
        {
            var residentialUnit = await _context.ResidentialUnits
                .Include(u => u.Blocks)
                    .ThenInclude(b => b.BlockManager)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (residentialUnit == null)
                return ApiResponse<ReturnResidentialUnitDto>.Error(
                    HttpStatusCode.NotFound, "لم يتم العثور على الوحدة السكنية"
                );

            return ApiResponse<ReturnResidentialUnitDto>.Success(
                residentialUnit.ToDto(), "تم جلب الوحدة السكنية بنجاح"
            );
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialUnitDto dto)
        {
            var unit = await _context.ResidentialUnits.FindAsync(id);
             if (unit == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الوحدة السكنية غير موجودة.");

             if (await _context.ResidentialUnits.AnyAsync(u => u.Name == dto.Name && u.Id != id))
                 return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "اسم الوحدة موجود مسبقاً.");

             unit.Name = dto.Name;
             await _context.SaveChangesAsync();
             
             return ApiResponse<string>.Success("تم تحديث الوحدة السكنية بنجاح.");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var unit = await _context.ResidentialUnits.Include(u => u.Blocks).FirstOrDefaultAsync(x => x.Id == id);
            if (unit == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الوحدة السكنية غير موجودة.");

            if (unit.Blocks.Any())
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "لا يمكن حذف الوحدة السكنية لوجود مربعات مرتبطة بها.");

            var managerId = unit.UnitManagerId;
            
            _context.ResidentialUnits.Remove(unit);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(managerId))
            {
                await _managerAccountService.DeleteManagerAccountAsync(managerId);
            }

            return ApiResponse<string>.Success("تم حذف الوحدة السكنية بنجاح.");
        }

        public async Task<ApiResponse<ReturnResidentialUnitDto>> ChangeManagerAsync(ChangeResidentialUnitManagerDto dto)
        {
            _logger.LogInformation("Initiating change of residential unit manager for UnitId: {UnitId}, PersonId: {PersonId}",
                dto.unitId, dto.PersonId);

            // Step 1: Validate unit
            var unit = await _context.ResidentialUnits.FindAsync(dto.unitId);
            if (unit == null)
            {
                _logger.LogWarning("Unit with ID '{UnitId}' not found.", dto.unitId);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.NotFound, "الوحدة السكنية غير موجودة");
            }

            // Step 2: Validate person
            var person = await _context.People.FindAsync(dto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", dto.PersonId);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود");
            }

            // Check if user already exists for this person
            var existingUserByPerson = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == dto.PersonId);
            if (existingUserByPerson != null)
            {
                _logger.LogWarning("User with PersonId {PersonId} already exists", dto.PersonId);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.BadRequest, "هذا المستخدم هو مدير بالفعل.");
            }

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Email '{Email}' is already used.", dto.Email);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.Conflict, "البريد الإلكتروني مستخدم مسبقاً.");
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
                         Role.UnitManager
                     );

                    if (!managerResult.IsSuccess)
                    {
                        return ApiResponse<ReturnResidentialUnitDto>.Error(managerResult.StatusCode, managerResult.Message, managerResult.Errors);
                    }
                    
                    var user = managerResult.Data;
                    var oldManagerId = unit.UnitManagerId;

                    // Step 5: Update unit manager
                    unit.UnitManagerId = user.Id;
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

                    await _context.Entry(unit).Reference(u => u.UnitManager).LoadAsync();
                    if (unit.UnitManager != null)
                    {
                        await _context.Entry(unit.UnitManager).Reference(um => um.Person).LoadAsync();
                    }

                    await transaction.CommitAsync();

                    _logger.LogInformation("Successfully changed manager for unit ID {UnitId} to new manager ID {NewManagerId}", dto.unitId, user.Id);

                    return ApiResponse<ReturnResidentialUnitDto>.Success(unit.ToDto(),
                        "تم تغيير مدير الوحدة السكنية بنجاح. تم إرسال بيانات الدخول عبر البريد الإلكتروني.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error changing manager for unit ID {UnitId}", dto.unitId);
                    return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.InternalServerError, "حدث خطأ غير متوقع أثناء تغيير المدير.");
                }
            });
        }

        public async Task<ApiResponse<ResidentialUnitDashboardDto>> GetDashboardAsync(
            CancellationToken ct = default)
        {
            var units = await _context.ResidentialUnits
                .AsNoTracking()
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    BlocksCount = u.Blocks.Count
                })
                .ToListAsync(ct);

            var dashboard = new ResidentialUnitDashboardDto
            {
                TotalUnits = units.Count,
                TotalBlocks = units.Sum(u => u.BlocksCount),
                Units = units.Select(u => new UnitStatsDto
                {
                    UnitId = u.Id,
                    UnitName = u.Name,
                    BlocksCount = u.BlocksCount
                }).ToList()
            };

            return ApiResponse<ResidentialUnitDashboardDto>.Success(dashboard);
        }
    }
}
