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
        private readonly IAuthService _authService;
        private readonly ILogger<ResidentialUnit> _logger;
        private readonly UserManager<AppUser> _userManager;


        public ResidentialUnitService(ApplicationDbContext context, IAuthService authService, ILogger<ResidentialUnit> logger, UserManager<AppUser> userManager)
        {
            _context = context;
            _authService = authService;
            _userManager = userManager;
            _logger = logger;
        }



        public async Task<ApiResponse<IEnumerable<ReturnResidentialUnitDto>>> GetAllAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User with ID '{UserId}' not found.", userId);
                return ApiResponse<IEnumerable<ReturnResidentialUnitDto>>.Error(
                    HttpStatusCode.NotFound, "لم يتم العثور على المستخدم"
                );
            }

            if (await _userManager.IsInRoleAsync(user, Role.Admin))
            {
                var residentialUnits = await _context.ResidentialUnits
                    .Include(u => u.Blocks)
                        .ThenInclude(b => b.BlockManager)
                    .Include(u => u.UnitManager)
                        .ThenInclude(um => um.Person)
                    .ToListAsync();

                // Map to DTO
                var residentialUnitDtos = residentialUnits.Select(u => MapToDto(u)).ToList();

                return ApiResponse<IEnumerable<ReturnResidentialUnitDto>>.Success(
                    residentialUnitDtos, "تم جلب الوحدات السكنية بنجاح"
                );
            }

            if (await _userManager.IsInRoleAsync(user, Role.UnitManager))
            {
                var residentialUnit = await _context.ResidentialUnits
                    .Include(u => u.Blocks)
                        .ThenInclude(b => b.BlockManager)
                    .Include(u => u.UnitManager)
                        .ThenInclude(um => um.Person)
                    .Where(u => u.UnitManagerId == user.Id)
                    .ToListAsync();

                if (!residentialUnit.Any())
                    return ApiResponse<IEnumerable<ReturnResidentialUnitDto>>.Error(
                        HttpStatusCode.NotFound, "لم يتم العثور على وحدة سكنية لهذا المستخدم"
                    );

                return ApiResponse<IEnumerable<ReturnResidentialUnitDto>>.Success(
                    residentialUnit.Select(u => MapToDto(u)).ToList(),
                    "تم جلب الوحدة السكنية الخاصة بك بنجاح"
                );
            }

            return ApiResponse<IEnumerable<ReturnResidentialUnitDto>>.Error(
                HttpStatusCode.Forbidden, "ليس لديك صلاحية للوصول للوحدات السكنية"
            );

        }

        public async Task<ApiResponse<ReturnResidentialUnitDto>> CreateAsync(AddResidentialUnitDto dto)
        {
            _logger.LogInformation("Attempting to create a new residential unit with name: {Name}", dto.Name);

            // Step 1: Validate residential neighborhood
            var neighborhood = await _context.ResidentialNeighborhoods.FindAsync(dto.ResidentialNeighborhoodId);
            if (neighborhood == null)
            {
                _logger.LogWarning("Residential neighborhood with ID {NeighborhoodId} not found", dto.ResidentialNeighborhoodId);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.NotFound, "الحي السكني غير موجود.");
            }

            // Step 2: Check if unit name already exists in this neighborhood
            var existingUnit = await _context.ResidentialUnits
                .FirstOrDefaultAsync(x => x.Name == dto.Name && x.ResidentialNeighborhoodId == dto.ResidentialNeighborhoodId);
            if (existingUnit != null)
            {
                _logger.LogWarning("Residential unit with name '{Name}' already exists in neighborhood {NeighborhoodId}", dto.Name, dto.ResidentialNeighborhoodId);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.Conflict, "اسم الوحدة السكنية موجود مسبقًا في هذا الحي.");
            }

            // Step 3: Validate person
            var person = await _context.People.FindAsync(dto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", dto.PersonId);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود.");
            }

            // Step 4: Check if user already exists for this person
            var existingUserByPerson = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == dto.PersonId);
            if (existingUserByPerson != null)
            {
                _logger.LogWarning("User with PersonId {PersonId} already exists", dto.PersonId);
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.BadRequest, "يوجد حساب مستخدم مرتبط بهذا الشخص مسبقاً");
            }

            // Step 5: Check if identifier already exists
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
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.Conflict, "المعرف (البريد الإلكتروني أو اسم المستخدم) مستخدم مسبقاً.");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Step 6: Create new manager account
                    var user = new AppUser
                    {
                        UserName = isEmail ? null : dto.Identifier,
                        Email = isEmail ? dto.Identifier : null,
                        PersonId = dto.PersonId,
                        IsActive = true,
                        EmailConfirmed = false
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

            _logger.LogInformation("Block manager updated successfully for block '{BlockName}' (ID: {BlockId})",
                block.Name, block.Id);

            return ApiResponse<RetrunBlockDto>.Success(returnBlockDto,
                "تم تحديث مدير المربع بنجاح. تم إرسال بيانات تسجيل الدخول عبر البريد الإلكتروني.");
        }
        public async Task<ApiResponse<RetrunBlockDto>> AddAsync(AddResidentialUnitDto blockDto)
        {
            _logger.LogInformation("Attempting to add a new block with name: {BlockName}", blockDto.Name);

                    if (!await _userManager.IsInRoleAsync(user, Role.UnitManager))
                    {
                        await _userManager.AddToRoleAsync(user, Role.UnitManager);
                    }

                    // Send Email Logic if identifier is email
                    if (isEmail)
                    {
                        var otp = new Random().Next(100000, 999999).ToString();
                        user.EmailConfirmationCode = otp;
                        user.EmailConfirmationCodeExpiresAt = DateTime.UtcNow.AddHours(1);
                        await _userManager.UpdateAsync(user);
                        await _emailSender.SendEmailAsync(user.Email, "تم إنشاء الحساب", $"تم إنشاء حسابك بنجاح. رمز التحقق هو: {otp}");
                    }

                    // Step 7: Create residential unit
                    var unit = new ResidentialUnit
                    {
                        Name = dto.Name,
                        ResidentialNeighborhoodId = dto.ResidentialNeighborhoodId,
                        UnitManagerId = user.Id
                    };

                    await _context.ResidentialUnits.AddAsync(unit);
                    await _context.SaveChangesAsync();

                    // Load navigation properties
                    await _context.Entry(unit).Reference(u => u.UnitManager).LoadAsync();
                    if (unit.UnitManager != null)
                    {
                        await _context.Entry(unit.UnitManager).Reference(um => um.Person).LoadAsync();
                    }

                    await transaction.CommitAsync();

                    _logger.LogInformation("Successfully created residential unit '{Name}' with ID {Id}", unit.Name, unit.Id);

                    return ApiResponse<ReturnResidentialUnitDto>.Success(
                        MapToDto(unit),
                        "تمت إضافة الوحدة السكنية بنجاح. تم إرسال بيانات الدخول عبر البريد الإلكتروني (إن وجد)."
                    );
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in CreateAsync");
                    return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء إضافة الوحدة السكنية.");
                }
            });
        }
        public async Task<ApiResponse<ReturnResidentialUnitDto>> GetByIdAsync(int id)
        {
            var residentialUnit = await _context.ResidentialUnits
                .Include(u => u.Blocks)
                    .ThenInclude(b => b.BlockManager)
                .Include(u => u.UnitManager)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (residentialUnit == null)
                return ApiResponse<ReturnResidentialUnitDto>.Error(
                    HttpStatusCode.NotFound, "لم يتم العثور على الوحدة السكنية"
                );

            return ApiResponse<ReturnResidentialUnitDto>.Success(
                MapToDto(residentialUnit), "تم جلب الوحدة السكنية بنجاح"
            );
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialUnitDto unitDto)
        {
            _logger.LogInformation("Attempting to update residential unit with ID: {UnitId}", id);

            var existingUnit = await _context.ResidentialUnits.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUnit == null)
            {
                _logger.LogWarning("Residential unit with ID {UnitId} not found", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الوحدة السكنية غير موجودة.");
            }

            existingUnit.Name = unitDto.Name;
            _context.ResidentialUnits.Update(existingUnit);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("Residential unit ID {UnitId} name updated to '{NewName}'", id, unitDto.Name);
                return ApiResponse<string>.Success(message: "تم تحديث اسم الوحدة السكنية بنجاح.");
            }

            _logger.LogError("Failed to update residential unit with ID {UnitId}", id);
            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "فشل في تحديث الوحدة السكنية.");
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var block = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == id);
            if (block == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المربع غير موجود.");

            //var userRole = await _authService.GetUserRole(block.UnitManagerId);
            //if (userRole != null && userRole == "BlockManager")
            //{
            //    var deleteResult = await _authService.DeleteBlockManagerAccountByIdAsync(block.UnitManagerId);
            //    if (!deleteResult.IsSuccess)
            //    {
            //        _logger.LogError("Failed to delete block manager with ID: {ManagerId}", block.UnitManagerId);
            //        return ApiResponse<string>.Error(deleteResult.StatusCode, deleteResult.Message, deleteResult.Errors);
            //    }
            //    _context.Blocks.Remove(block);
            //    return ApiResponse<string>.Success("تم حذف المربع بنجاح.");
            //}

            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "فشل في حذف المربع.");
        }
        public async Task<ApiResponse<BlockDetailesDto>> GetDetails(int blockId, int pageNumber, int pageSize, string? search)
        {
            var block = await _context.Blocks
                .AsNoTracking()
                .Where(x => x.Id == blockId)
                .Select(x => new BlockDetailesDto
                {
                    Block = new BlockWithStatsDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        //ManagerName = x.UnitManager.Person.FullName,
                        TotalFamilies = x.Families.Count,
                        totalOrphans = x.Families.Count(f => f.FamilyCatgory.Id == 2),
                        TotalWidows = x.Families.Count(f => f.FamilyCatgory.Id == 1),
                    },
                    Families = x.Families.Select(f => new FamilyDetailsDto
                    {
                        Id = f.Id,
                        Name = f.Name,
                        FamilyCatgoryId = f.FamilyCatgoryId,
                        FamilyCatgoryName = f.FamilyCatgory.Name,
                        BlockId = f.BlockId,
                        BlockName = f.Block.Name,

                        FamilyNotes = f.FamilyNotes,
                        Location = f.Location,
                        FamilyHeadId = f.FamilyMembers
                            .Where(fm => fm.MemberFamilyRoleId == 1)
                            .Select(fm => fm.PersonId)
                            .FirstOrDefault(),

                        FamilyHeadName = f.FamilyMembers
                            .Where(fm => fm.MemberFamilyRoleId == 1)
                            .Select(fm => fm.Person.FullName)
                            .FirstOrDefault() ?? string.Empty,

                        PhoneNumber = f.FamilyMembers
                            .Where(fm => fm.MemberFamilyRoleId == 1)
                            .Select(fm => fm.Person.PhoneNumber)
                            .FirstOrDefault() ?? string.Empty,

                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (block == null)
            {
                _logger.LogWarning("Block with ID {BlockId} not found", blockId);
                return ApiResponse<BlockDetailesDto>.Error(HttpStatusCode.NotFound, "المربع غير موجود.");
            }

            return ApiResponse<BlockDetailesDto>.Success(block, "تم جلب تفاصيل المربع بنجاح.");
        }

        private ReturnResidentialUnitDto MapToDto(ResidentialUnit unit)
        {
            return new ReturnResidentialUnitDto
            {
                Id = unit.Id,
                Name = unit.Name,
                UnitManagerId = unit.UnitManagerId,
                UnitManagerName = unit.UnitManager?.UserName ?? string.Empty,
                Blocks = unit.Blocks.Select(b => new Block
                {
                    Id = b.Id,
                    Name = b.Name,
                    BlockManagerId = b.BlockManagerId
                }).ToList()
            };
        }



    }
}
