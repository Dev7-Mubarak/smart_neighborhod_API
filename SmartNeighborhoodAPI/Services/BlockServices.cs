using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Interfaces;
using System.Data;
using System.Linq;
using System.Net;
using static SmartNeighborhoodAPI.Helpers.Router;

namespace SmartNeighborhoodAPI.Services
{
    public class BlockServices : IBlockServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ILogger<Block> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserContextService _userContextService;


        public BlockServices(ApplicationDbContext context, IMapper mapper, IAuthService authService, ILogger<Block> logger, UserManager<AppUser> userManager, UserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
            _logger = logger;
            _userManager = userManager;
            _userContextService = userContextService;
        }

        public async Task<ApiResponse<IEnumerable<RetrunBlockDto>>> GetAllAsync()
        {
            CurrentUserDto currentUser;

            try
            {
                currentUser = _userContextService.GetCurrentUser();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Failed to resolve current user for fetching blocks.");
                return ApiResponse<IEnumerable<RetrunBlockDto>>.Error(
                    HttpStatusCode.Unauthorized,
                    "يجب تسجيل الدخول للوصول إلى هذه البيانات.");
            }

            _logger.LogInformation("Fetching blocks for user ID: {UserId} with role {Role}", currentUser.Id, currentUser.Role);

            IQueryable<Block> query = _context.Blocks.AsQueryable();

            if (currentUser.Role == Role.BlockManager)
            {
                var rootBlockIds = await _context.Blocks
                    //.Where(b => b.UnitManagerId == currentUser.Id)
                    .Select(b => b.Id)
                    .ToListAsync();

                if (!rootBlockIds.Any())
                {
                    _logger.LogInformation("Block manager {UserId} has no managed blocks.", currentUser.Id);
                    return ApiResponse<IEnumerable<RetrunBlockDto>>
                        .Success(Enumerable.Empty<RetrunBlockDto>(), "لا توجد بيانات متاحة.");
                }


                var flatBlocks = await _context.Blocks
                    .Select(b => new { b.Id, b.BlockManagerId })
                    .ToListAsync();

                var allowedIds = new HashSet<int>(rootBlockIds);
                var queue = new Queue<int>(rootBlockIds);

                while (queue.Count > 0)
                {
                    var currentId = queue.Dequeue();

                    var children = flatBlocks
                        //.Where(x => x.BlockManagerId == currentId)
                        .Select(x => x.Id);

                    foreach (var childId in children)
                    {
                        if (allowedIds.Add(childId))
                            queue.Enqueue(childId);
                    }
                }

                query = _context.Blocks.Where(b => allowedIds.Contains(b.Id));
            }
            else if (currentUser.Role == Role.BlockManager)
            {
                _logger.LogWarning("User {UserId} attempted to access blocks without permission", currentUser.Id);
                return ApiResponse<IEnumerable<RetrunBlockDto>>.Error(
                    HttpStatusCode.Unauthorized,
                    "ليس لديك صلاحية الوصول إلى هذه البيانات.");
            }

            var blocks = await query
                .Select(b => new RetrunBlockDto
                {
                    Id = b.Id,
                    //ManagerId = b.UnitManagerId,
                    Role = currentUser.Role,
                    Name = b.Name,
                    Identifier = currentUser.Email,
                    //PersonId = b.UnitManager.PersonId,
                    //FullName = b.UnitManager.Person.FullName
                })
                .AsNoTracking()
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} blocks for user {UserId}", blocks.Count, currentUser.Id);

            string message = blocks.Any()
                ? "تم جلب جميع البيانات بنجاح."
                : "لا توجد بيانات متاحة.";

            return ApiResponse<IEnumerable<RetrunBlockDto>>.Success(blocks, message);
        }


        public async Task<ApiResponse<RetrunBlockDto>> ChangeManager(int id, ChangeManagerDto dto)
        {
            _logger.LogInformation("Initiating change of block manager for BlockId: {BlockId}, PersonId: {PersonId}",
                id, dto.PersonId);

            // Step 1: Validate block
            var block = await _context.Blocks.FindAsync(id);
            if (block == null)
            {
                _logger.LogWarning("Block with ID '{BlockId}' not found.", id);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على مربع.");
            }

            // Step 2: Validate person
            var person = await _context.People.FindAsync(dto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", dto.PersonId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود");
            }

            // Check if user already exists for this person
            var existingUserByPerson = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == dto.PersonId);
            if (existingUserByPerson != null)
            {
                _logger.LogWarning("User with PersonId {PersonId} already exists", dto.PersonId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.BadRequest, "هذا المستخدم هو مدير بالفعل.");
            }

            // Check if identifier already exists
            AppUser existingUser = await _userManager.FindByNameAsync(dto.Identifier);

            if (existingUser != null)
            {
                _logger.LogWarning("Identifier '{Identifier}' is already used.", dto.Identifier);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "المعرف (البريد الإلكتروني أو اسم المستخدم) مستخدم مسبقاً.");
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
                        UserName = dto.Identifier,
                        Email = null,
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
                        _logger.LogError("Failed to create new block manager. Reason: {Reason}", string.Join(", ", errors.Select(e => e.ErrorMessage)));
                        return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.BadRequest, "حدث خطأ أثناء إنشاء المستخدم.", errors);
                    }

                    if (!await _userManager.IsInRoleAsync(user, Role.BlockManager))
                    {
                        await _userManager.AddToRoleAsync(user, Role.BlockManager);
                    }

                    var oldManagerId = block.BlockManagerId;
                    var oldManagerUser = await _userManager.FindByIdAsync(oldManagerId);

                    // Step 5: Update block manager
                    block.BlockManagerId = user.Id;
                    await _context.SaveChangesAsync();

                    // Step 6: Delete old manager account (if any)
                    if (oldManagerUser != null)
                    {
                        var deleteResult = await _userManager.DeleteAsync(oldManagerUser);
                        if (!deleteResult.Succeeded)
                        {
                            var errors = deleteResult.Errors.Select(e => new ErrorDetails { Field = e.Code, ErrorMessage = e.Description }).ToList();
                            _logger.LogError("Failed to delete old block manager with ID: {OldManagerId}", oldManagerUser.Id);
                            // Rollback is handled by the catch block
                            throw new Exception("فشل حذف المدير القديم");
                        }
                    }

                    await _context.Entry(block).Reference(e => e.BlockManager).LoadAsync();
                    if (block.BlockManager != null)
                    {
                        await _context.Entry(block.BlockManager).Reference(nm => nm.Person).LoadAsync();
                    }

                    await transaction.CommitAsync();

                    _logger.LogInformation("Block manager updated successfully for block '{Name}' (ID: {Id})",
                        block.Name, block.Id);

                    var returnBlockDto = new RetrunBlockDto
                    {
                        Id = block.Id,
                        Name = block.Name,
                        ManagerId = user.Id,
                        PersonId = person.Id,
                        Identifier = dto.Identifier,
                        Role = Role.BlockManager,
                        FullName = person.FullName,
                        ResitinalUnitId = block.ResidentialUnitId
                    };

                    return ApiResponse<RetrunBlockDto>.Success(returnBlockDto,
                        "تم تغيير مدير المربع بنجاح.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in ChangeManager");
                    return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء معالجة الطلب.");
                }
            });
        }
        public async Task<ApiResponse<RetrunBlockDto>> AddAsync(BlockDto blockDto)
        {
            _logger.LogInformation("Attempting to add a new block with name: {BlockName}", blockDto.Name);

            var existblock = await _context.Blocks.FirstOrDefaultAsync(x => x.Name == blockDto.Name);
            if (existblock != null)
            {
                _logger.LogWarning("Block with name '{BlockName}' already exists", blockDto.Name);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "اسم المربع موجود مسبقًا.");
            }

            var existIdentifier = await _userManager.FindByNameAsync(blockDto.Identifier);
            if (existIdentifier != null)
            {
                _logger.LogWarning("Identifier '{Identifier}' is already used", blockDto.Identifier);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "المعرف (البريد الإلكتروني أو اسم المستخدم) مستخدم مسبقاً");
            }
            var existResidentialUnit = await _context.ResidentialUnits.FindAsync(blockDto.ResitinalUnitId);
            if (existResidentialUnit == null)
            {
                _logger.LogWarning("Residential Unit with ID {ResidentialUnitId} not found", blockDto.ResitinalUnitId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "الوحدة السكنية غير موجودة.");
            }

            var person = await _context.People.FindAsync(blockDto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", blockDto.PersonId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود.");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    CreateBlockManagerDto blockManagerDto = new CreateBlockManagerDto
                    {
                        Identifier = blockDto.Identifier,
                        PersonId = blockDto.PersonId,
                        Password = blockDto.Password,
                        ResitinalUnitId = blockDto.ResitinalUnitId
                    };

                    var response = await _authService.CreateBlockManagerAccountAsync(blockManagerDto);

                    if (!response.IsSuccess)
                    {
                        _logger.LogError("Failed to create block manager: {Error}", response.Message);
                        return ApiResponse<RetrunBlockDto>.Error(response.StatusCode, response.Message, response.Errors);
                    }

                    var block = new Block
                    {
                        Name = blockDto.Name,
                        BlockManagerId = response.Data.Id,
                        ResidentialUnit = existResidentialUnit
                    };

                    await _context.Blocks.AddAsync(block);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    // Refactor and improve performance
                    var retrunBlock = new RetrunBlockDto
                    {
                        Id = block.Id,
                        Name = blockDto.Name,
                        PersonId = blockDto.PersonId,
                        ManagerId = response.Data.Id,
                        Role = response.Data.Role,
                        Identifier = response.Data.Identifier,
                        FullName = person.FullName,
                        ResitinalUnitId = blockDto.ResitinalUnitId

                    };

                    _logger.LogInformation("Successfully added block '{BlockName}' with ID {BlockId}", block.Name, block.Id);
                    return ApiResponse<RetrunBlockDto>.Success(
                        retrunBlock,
                        "تمت إضافة البلوك بنجاح. تم إرسال رمز التأكيد إلى البريد الإلكتروني."
                    );
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in AddAsync");
                    return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء إضافة البلوك.");
                }
            });
        }
        public async Task<ApiResponse<Block>> GetByIdAsync(int id)
        {
            var block = await _context.Blocks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (block == null)
                return ApiResponse<Block>.Error(HttpStatusCode.NotFound, "المربع غير موجود.");

            return ApiResponse<Block>.Success(block, "تم جلب بيانات المربع بنجاح.");
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateBlockDto blockDto)
        {
            _logger.LogInformation("Attempting to update block with ID: {BlockId}", id);

            var existingBlock = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlock == null)
            {
                _logger.LogWarning("Block with ID {BlockId} not found", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المربع غير موجود.");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    existingBlock.Name = blockDto.Name;
                    _context.Blocks.Update(existingBlock);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    _logger.LogInformation("Block ID {BlockId} name updated to '{NewName}'", id, blockDto.Name);
                    return ApiResponse<string>.Success(message: "تم تحديث اسم المربع بنجاح.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in UpdateAsync");
                    return ApiResponse<string>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء تحديث المربع.");
                }
            });
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var block = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == id);
            if (block == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المربع غير موجود.");

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _context.Blocks.Remove(block);
                    await _context.SaveChangesAsync();

                    if (block.BlockManagerId != null)
                    {
                        var userRole = await _authService.GetUserRole(block.BlockManagerId);
                        if (userRole != null && userRole == "BlockManager")
                        {
                            var deleteResult = await _authService.DeleteBlockManagerAccountByIdAsync(block.BlockManagerId);
                            if (!deleteResult.IsSuccess)
                            {
                                _logger.LogError("Failed to delete block manager with ID: {ManagerId}", block.BlockManagerId);
                                return ApiResponse<string>.Error(deleteResult.StatusCode, deleteResult.Message, deleteResult.Errors);
                            }
                        }
                    }

                    await transaction.CommitAsync();

                    return ApiResponse<string>.Success("تم حذف المربع بنجاح.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in DeleteAsync");
                    return ApiResponse<string>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء حذف المربع.");
                }
            });
        }



        public async Task<ApiResponse<BlockDashboardDto>> GetDashboardAsync(CancellationToken ct = default)
        {
            var blocks = await _context.Blocks
                .AsNoTracking()
                .Select(b => new
                {
                    b.Id,
                    b.Name,
                    FamiliesCount = b.Families.Count,
                    ManagerId = b.BlockManagerId,
                    ManagerName = b.BlockManager.Person.FullName
                })
                .ToListAsync(ct);

            var dashboard = new BlockDashboardDto
            {
                TotalBlocks = blocks.Count,
                TotalFamilies = blocks.Sum(b => b.FamiliesCount),
                Blocks = blocks.Select(b => new BlockStatsDto
                {
                    BlockId = b.Id,
                    BlockName = b.Name,
                    FamiliesCount = b.FamiliesCount,
                    ManagerId = b.ManagerId,
                    ManagerName = b.ManagerName
                }).ToList()
            };

            return ApiResponse<BlockDashboardDto>.Success(dashboard, "تم جلب إحصائيات المربعات بنجاح");
        }

        public async Task<ApiResponse<BlockDashboardDto>> GetMyDashboardAsync(CancellationToken ct = default)
        {
            var currentUser = _userContextService.GetCurrentUser();
            var userId = currentUser.Id;

            _logger.LogInformation("Fetching dashboard statistics for block manager with userId: {UserId}", userId);

            var blocks = await _context.Blocks
                .AsNoTracking()
                .Where(b => b.BlockManagerId == userId)
                .Include(b => b.BlockManager)
                    .ThenInclude(bm => bm.Person)
                .ToListAsync(ct);

            if (!blocks.Any())
            {
                _logger.LogWarning("No blocks found for manager with userId: {UserId}", userId);
                var empty = new BlockDashboardDto
                {
                    TotalBlocks = 0,
                    TotalFamilies = 0,
                    Blocks = new List<BlockStatsDto>()
                };
                return ApiResponse<BlockDashboardDto>.Success(empty, "لا توجد مربعات مرتبطة بهذا المدير");
            }


            var dashboard = new BlockDashboardDto
            {
                TotalBlocks = blocks.Count,
                TotalFamilies = blocks.Sum(b => b.Families.Count),
                Blocks = blocks.Select(b => new BlockStatsDto
                {
                    BlockId = b.Id,
                    BlockName = b.Name,
                    FamiliesCount = b.Families.Count,
                    ManagerId = b.BlockManagerId,
                    ManagerName = b.BlockManager?.Person?.FullName ?? string.Empty
                }).ToList()
            };

            _logger.LogInformation("Dashboard statistics retrieved successfully for block manager {UserId}: {Blocks} blocks", userId, dashboard.TotalBlocks);

            return ApiResponse<BlockDashboardDto>.Success(dashboard, "تم جلب إحصائيات لوحة التحكم بنجاح");
        }

        public async Task<ApiResponse<List<RetrunBlockDto>>> GetMyBlocksAsync(string userId, CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching blocks for manager with userId: {UserId}", userId);

            var blocks = await _context.Blocks
                .AsNoTracking()
                .Where(b => b.BlockManagerId == userId)
                .Include(b => b.BlockManager)
                    .ThenInclude(bm => bm.Person)
                .Include(b => b.ResidentialUnit)
                .OrderBy(b => b.Name)
                .ToListAsync(ct);

            if (!blocks.Any())
            {
                _logger.LogWarning("No blocks found for manager with userId: {UserId}", userId);
                return ApiResponse<List<RetrunBlockDto>>.Success(
                    new List<RetrunBlockDto>(),
                    "لا توجد مربعات مرتبطة بهذا المدير"
                );
            }

            var result = blocks.Select(b => new RetrunBlockDto
            {
                Id = b.Id,
                Name = b.Name,
                ManagerId = b.BlockManagerId,
                PersonId = b.BlockManager.PersonId,
                Identifier = b.BlockManager.UserName ?? string.Empty,
                Role = Role.BlockManager,
                FullName = b.BlockManager.Person?.FullName ?? string.Empty
            }).ToList();

            _logger.LogInformation("Retrieved {Count} blocks for manager with userId: {UserId}", result.Count, userId);

            return ApiResponse<List<RetrunBlockDto>>.Success(result, "تم جلب المربعات بنجاح");
        }

        public async Task<ApiResponse<ReturnBlockFamiliesDto>> GetFamiliesAsync(int id)
        {
            var block = await _context.Blocks
                .Include(b => b.BlockManager)
                    .ThenInclude(bm => bm.Person)
                .Include(b => b.Families)
                    .ThenInclude(f => f.FamilyCatgory)
                .Include(b => b.Families)
                    .ThenInclude(f => f.FamilyMembers)
                        .ThenInclude(fm => fm.Person)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (block == null)
            {
                return ApiResponse<ReturnBlockFamiliesDto>.Error(
                    HttpStatusCode.NotFound,
                    "المربع غير موجود"
                );
            }

            var result = new ReturnBlockFamiliesDto
            {
                Id = block.Id,
                Name = block.Name,
                BlockManagerId = block.BlockManagerId,
                BlockManagerName = block.BlockManager?.Person?.FullName ?? string.Empty,
                Families = block.Families.Select(f =>
                {
                    // Find family head: MemberFamilyRoleId == 1 (أب)
                    var head = f.FamilyMembers.FirstOrDefault(x => x.MemberFamilyRoleId == 1);
                    return new FamilySummaryDto
                    {
                        Id = f.Id,
                        Name = f.Name,
                        Location = f.Location,
                        FamilyCategoryId = f.FamilyCatgoryId,
                        FamilyCategoryName = f.FamilyCatgory?.Name ?? string.Empty,
                        FamilyHeadId = head?.PersonId,
                        FamilyHeadName = head?.Person?.FullName ?? string.Empty,
                        FamilyNotes = f.FamilyNotes ?? string.Empty
                    };
                }).ToList()
            };

            return ApiResponse<ReturnBlockFamiliesDto>.Success(result, "تم جلب العائلات بنجاح");
        }

    }
}
