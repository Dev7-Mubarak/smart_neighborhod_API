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
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ResidentialNeighborhoodService> _logger;
        private readonly UserContextService _userContextService;

        public ResidentialNeighborhoodService(ApplicationDbContext context, UserManager<AppUser> userManager, IEmailSender emailSender, ILogger<ResidentialNeighborhoodService> logger, UserContextService userContextService)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _userContextService = userContextService;
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

            // Create Manager Account Logic
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == dto.PersonId);

            if (user == null)
            {
                user = new AppUser
                {
                    UserName = dto.Identifier,
                    Email = dto.Identifier,
                    PersonId = dto.PersonId,
                    IsActive = true,
                    EmailConfirmed = false
                };

                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => new ErrorDetails { Field = e.Code, ErrorMessage = e.Description }).ToList();
                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "فشل إنشاء حساب المستخدم", errors);
                }
            }

            if (!await _userManager.IsInRoleAsync(user, Role.ResidentialNeighborhoodManager))
            {
                await _userManager.AddToRoleAsync(user, Role.ResidentialNeighborhoodManager);
            }

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


            _logger.LogInformation("Successfully created residential neighborhood '{Name}' with ID {Id}", entity.Name, entity.Id);

            return ApiResponse<ReturnResidentialNeighborhoodDto>.Success(entity.ToDto(), "تم إنشاء الحي السكني بنجاح");

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
                        var oldManagerUser = await _userManager.FindByIdAsync(managerId);
                        if (oldManagerUser != null)
                        {
                            var deleteResult = await _userManager.DeleteAsync(oldManagerUser);
                            if (!deleteResult.Succeeded)
                            {
                                var errors = deleteResult.Errors.Select(e => new ErrorDetails { Field = e.Code, ErrorMessage = e.Description }).ToList();
                                _logger.LogError("Failed to delete old residential neighborhood manager with ID: {OldManagerId}", oldManagerUser.Id);
                                // Rollback is handled by the catch block
                                throw new Exception("فشل حذف المدير القديم");
                            }
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

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> ChangeManagerAsync(int id, ChangeResidentialManagerDto dto)
        {
            _logger.LogInformation("Initiating change of residential neighborhood manager for NeighborhoodId: {NeighborhoodId}, PersonId: {PersonId}",
                id, dto.PersonId);

            // Step 1: Validate neighborhood
            var neighborhood = await _context.ResidentialNeighborhoods.FindAsync(id);
            if (neighborhood == null)
            {
                _logger.LogWarning("Neighborhood with ID '{NeighborhoodId}' not found.", id);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "الحي السكني غير موجود");
            }

            // Step 2: Validate person
            var person = await _context.People.FindAsync(dto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", dto.PersonId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود");
            }

            // Check if user already exists for this person
            var existingUserByPerson = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == dto.PersonId);
            if (existingUserByPerson != null)
            {
                _logger.LogWarning("User with PersonId {PersonId} already exists", dto.PersonId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "هذا المستخدم هو مدير بالفعل.");
            }

            // Check if identifier already exists
            bool isEmail = dto.Identifier.Contains('@');
            AppUser existingUser = null;

            if (isEmail)
            {
                existingUser = await _userManager.FindByEmailAsync(dto.Identifier);
            }
            else
            {
                existingUser = await _userManager.FindByNameAsync(dto.Identifier);
            }

            if (existingUser != null)
            {
                _logger.LogWarning("Identifier '{Identifier}' is already used.", dto.Identifier);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.Conflict, "المعرف (البريد الإلكتروني أو اسم المستخدم) مستخدم مسبقاً.");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Step 4: Create new manager account
                    var user = new AppUser
                    {
                        UserName = isEmail ? null : dto.Identifier,
                        Email = isEmail ? dto.Identifier : null,
                        PersonId = dto.PersonId,
                        IsActive = true,
                        EmailConfirmed = true
                    };

                    var createResult = await _userManager.CreateAsync(user, dto.Password);

                    if (!createResult.Succeeded)
                    {
                        List<ErrorDetails> errors = createResult.Errors.Select(e =>
                        {
                            string arabicMessage = e.Code switch
                            {
                                "DuplicateUserName" => "البريد الإلكتروني أو اسم المستخدم مستخدم مسبقاً.",
                                "InvalidUserName" => "اسم المستخدم غير صالح.",
                                "PasswordTooShort" => "كلمة المرور قصيرة جداً.",
                                "PasswordRequiresNonAlphanumeric" => "كلمة المرور يجب أن تحتوي على رمز خاص.",
                                "PasswordRequiresDigit" => "كلمة المرور يجب أن تحتوي على رقم.",
                                "PasswordRequiresLower" => "كلمة المرور يجب أن تحتوي على حرف صغير.",
                                "PasswordRequiresUpper" => "كلمة المرور يجب أن تحتوي على حرف كبير.",
                                "PasswordIsRequired" => "كلمة المرور مطلوبة.",
                                _ => e.Description
                            };

                            return new ErrorDetails
                            {
                                Field = e.Code,
                                ErrorMessage = arabicMessage
                            };
                        }).ToList();
                        _logger.LogError("Failed to create new residential neighborhood manager. Reason: {Reason}", string.Join(", ", errors.Select(e => e.ErrorMessage)));
                        return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "حدث خطأ أثناء إنشاء المستخدم.", errors);
                    }

                    if (!await _userManager.IsInRoleAsync(user, Role.ResidentialNeighborhoodManager))
                    {
                        await _userManager.AddToRoleAsync(user, Role.ResidentialNeighborhoodManager);
                    }

                    // Send Email Logic (Simplified)
                    if (isEmail)
                    {
                        var otp = new Random().Next(100000, 999999).ToString();
                        user.EmailConfirmationCode = otp;
                        user.EmailConfirmationCodeExpiresAt = DateTime.UtcNow.AddHours(1);
                        await _userManager.UpdateAsync(user);
                        await _emailSender.SendEmailAsync(user.Email, "تم إنشاء الحساب", $"تم إنشاء حسابك بنجاح. رمز التحقق هو: {otp}");
                    }


                    var oldManagerId = neighborhood.NeighborhoodManagerId;
                    var oldManagerUser = await _userManager.FindByIdAsync(oldManagerId);

                    // Step 5: Update neighborhood manager
                    neighborhood.NeighborhoodManagerId = user.Id;
                    await _context.SaveChangesAsync();

                    // Step 6: Delete old manager account (if any)
                    if (oldManagerUser != null)
                    {
                        var deleteResult = await _userManager.DeleteAsync(oldManagerUser);
                        if (!deleteResult.Succeeded)
                        {
                            var errors = deleteResult.Errors.Select(e => new ErrorDetails { Field = e.Code, ErrorMessage = e.Description }).ToList();
                            _logger.LogError("Failed to delete old residential neighborhood manager with ID: {OldManagerId}", oldManagerUser.Id);
                            // Rollback is handled by the catch block
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
                .Include(n => n.NeighborhoodManager)
                    .ThenInclude(nm => nm.Person)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .ToListAsync(ct);

            var dashboard = new ResidentialDashboardDto
            {
                TotalNeighborhoods = neighborhoods.Count,
                TotalUnits = neighborhoods.Sum(n => n.ResidentialUnits.Count),
                TotalBlocks = neighborhoods.Sum(n => n.ResidentialUnits.Sum(u => u.Blocks.Count)),
                Neighborhoods = neighborhoods.Select(n => new NeighborhoodStatsDto
                {
                    NeighborhoodId = n.Id,
                    NeighborhoodName = n.Name,
                    UnitsCount = n.ResidentialUnits.Count,
                    BlocksCount = n.ResidentialUnits.Sum(u => u.Blocks.Count),
                    ManagerId = n.NeighborhoodManagerId,
                    ManagerName = n.NeighborhoodManager?.Person?.FullName ?? string.Empty
                }).ToList()
            };

            return ApiResponse<ResidentialDashboardDto>.Success(dashboard);
        }

        public async Task<ApiResponse<ReturnResidentialUnitDto>> GetUnitsAsync(int id)
        {
            var entity = await _context.ResidentialNeighborhoods
                .Include(n => n.NeighborhoodManager)
                    .ThenInclude(nm => nm.Person)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(n => n.UnitManager)
                        .ThenInclude(um => um.Person)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (entity == null)
                return ApiResponse<ReturnResidentialUnitDto>
                        .Error(HttpStatusCode.NotFound, "الحي السكني غير موجود");


            return ApiResponse<ReturnResidentialUnitDto>.Success(entity.ToResidentialUnitDto(), "تم جلب الوحدات السكنية بنجاح");
        }

        public async Task<ApiResponse<ResidentialDashboardDto>> GetMyDashboardAsync(CancellationToken ct = default)
        {
            var currentUser = _userContextService.GetCurrentUser();
            var userId = currentUser.Id;
            _logger.LogInformation("Fetching dashboard statistics for manager with userId: {UserId}", userId);

            var neighborhoods = await _context.ResidentialNeighborhoods
                .AsNoTracking()
                .Include(n => n.NeighborhoodManager)
                    .ThenInclude(nm => nm.Person)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .Where(n => n.NeighborhoodManagerId == userId)
                .ToListAsync(ct);

            if (!neighborhoods.Any())
            {
                _logger.LogWarning("No neighborhoods found for manager with userId: {UserId}", userId);
                var empty = new ResidentialDashboardDto
                {
                    TotalNeighborhoods = 0,
                    TotalUnits = 0,
                    TotalBlocks = 0,
                    Neighborhoods = new List<NeighborhoodStatsDto>()
                };
                return ApiResponse<ResidentialDashboardDto>.Success(empty, "لا توجد أحياء سكنية مرتبطة بهذا المدير");
            }

            var dashboard = new ResidentialDashboardDto
            {
                TotalNeighborhoods = neighborhoods.Count,
                TotalUnits = neighborhoods.Sum(n => n.ResidentialUnits.Count),
                TotalBlocks = neighborhoods.Sum(n => n.ResidentialUnits.Sum(u => u.Blocks.Count)),
                Neighborhoods = neighborhoods.Select(n => new NeighborhoodStatsDto
                {
                    NeighborhoodId = n.Id,
                    NeighborhoodName = n.Name,
                    UnitsCount = n.ResidentialUnits.Count,
                    BlocksCount = n.ResidentialUnits.Sum(u => u.Blocks.Count),
                    ManagerId = n.NeighborhoodManagerId,
                    ManagerName = n.NeighborhoodManager?.Person?.FullName ?? string.Empty
                }).ToList()
            };

            _logger.LogInformation("Dashboard statistics retrieved successfully for manager {UserId}: {Neighborhoods} neighborhoods", userId, dashboard.TotalNeighborhoods);

            return ApiResponse<ResidentialDashboardDto>.Success(dashboard, "تم جلب إحصائيات لوحة التحكم بنجاح");
        }

        public async Task<ApiResponse<List<ReturnResidentialUnitDto>>> GetMyNeighborhoodsAsync(string userId, CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching neighborhoods for manager with userId: {UserId}", userId);

            var neighborhoods = await _context.ResidentialNeighborhoods
                .AsNoTracking()
                .Where(n => n.NeighborhoodManagerId == userId)
                .Include(n => n.NeighborhoodManager)
                    .ThenInclude(nm => nm.Person)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.UnitManager)
                        .ThenInclude(um => um.Person)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .Where(n => n.ResidentialUnits.Any())
                .OrderBy(n => n.Name)
                .ToListAsync(ct);

            if (!neighborhoods.Any())
            {
                _logger.LogWarning("No neighborhoods found for manager with userId: {UserId}", userId);
                return ApiResponse<List<ReturnResidentialUnitDto>>.Success(
                    new List<ReturnResidentialUnitDto>(),
                    "لا توجد أحياء سكنية مرتبطة بهذا المدير"
                );
            }

            var result = neighborhoods.Select(n => n.ToResidentialUnitDto()).ToList();

            _logger.LogInformation("Retrieved {Count} neighborhoods for manager with userId: {UserId}", result.Count, userId);

            return ApiResponse<List<ReturnResidentialUnitDto>>.Success(result, "تم جلب الأحياء السكنية بنجاح");
        }
    }

}
