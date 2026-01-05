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


        public async Task<ApiResponse<RetrunBlockDto>> ChangeManager(int id, ChangeManagerDto blockManagerDto)
        {
            _logger.LogInformation("Initiating change of block manager for BlockId: {BlockId}, PersonId: {PersonId}",
                id, blockManagerDto.PersonId);

            // Step 1: Validate block
            var block = await _context.Blocks.FindAsync(id);
            if (block == null)
            {
                _logger.LogWarning("Block with ID '{BlockId}' not found.", id);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على مربع.");
            }

            // Step 2: Validate person
            var person = await _context.People.FindAsync(blockManagerDto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", blockManagerDto.PersonId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "هذا الشخص غير موجود");
            }

            var existingUser = await _userManager.FindByEmailAsync(blockManagerDto.Email);

            if (existingUser != null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", blockManagerDto.Email);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "هذا الايميل مستخدم بالفعل ");
            }

            // Step 4: Create new manager account
            var createResult = await _authService.CreateBlockManagerAccountAsync(new CreateBlockManagerDto
            {
                Email = blockManagerDto.Email, // This is actually used as UserName now
                Password = blockManagerDto.Password,
                PersonId = blockManagerDto.PersonId
            });

            if (!createResult.IsSuccess)
            {
                _logger.LogError("Failed to create new block manager. Reason: {Reason}", createResult.Message);
                return ApiResponse<RetrunBlockDto>.Error(createResult.StatusCode, createResult.Message, createResult.Errors);
            }


            var oldManagerId = block.BlockManagerId;


            // Step 5: Update block manager
            //block.UnitManagerId = createResult.Data.Id;
            _context.Blocks.Update(block);
            await _context.SaveChangesAsync();

            if (createResult.Data.Role == "BlockManager")
            {
                // Step 6: Delete old manager account (if any)
                var deleteResult = await _authService.DeleteBlockManagerAccountByIdAsync(oldManagerId);
                if (!deleteResult.IsSuccess)
                {
                    _logger.LogError("Failed to delete old block manager with ID: {OldManagerId}", oldManagerId);
                    return ApiResponse<RetrunBlockDto>.Error(deleteResult.StatusCode, deleteResult.Message, deleteResult.Errors);
                }
            }


            // Step 7: Return success response
            var returnBlockDto = new RetrunBlockDto
            {
                Id = block.Id,
                Name = block.Name,
                //ManagerId = block.UnitManagerId,    
                PersonId = person.Id,
                Email = createResult.Data.Email,
                Role = createResult.Data.Role,
                FullName = person.FullName
            };

            _logger.LogInformation("Block manager updated successfully for block '{BlockName}' (ID: {BlockId})",
                block.Name, block.Id);

            return ApiResponse<RetrunBlockDto>.Success(returnBlockDto,
                "تم تحديث مدير المربع بنجاح. تم إرسال بيانات تسجيل الدخول عبر البريد الإلكتروني.");
        }
        public async Task<ApiResponse<RetrunBlockDto>> AddAsync(AddResidentialUnitDto blockDto)
        {
            _logger.LogInformation("Attempting to add a new block with name: {BlockName}", blockDto.Name);

            var existblock = await _context.Blocks.FirstOrDefaultAsync(x => x.Name == blockDto.Name);
            if (existblock != null)
            {
                _logger.LogWarning("Block with name '{BlockName}' already exists", blockDto.Name);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "اسم المربع موجود مسبقًا.");
            }

            var person = await _context.People.FindAsync(blockDto.UnitManagerId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", blockDto.UnitManagerId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود.");
            }

            CreateBlockManagerDto blockManagerDto = new CreateBlockManagerDto
            {
                //Email = blockDto.Email,
                //PersonId = blockDto.PersonId,
                //Password = blockDto.Password
            };

            var response = await _authService.CreateBlockManagerAccountAsync(blockManagerDto);

            if (!response.IsSuccess)
            {
                return ApiResponse<RetrunBlockDto>.Error(response.StatusCode, response.Message, response.Errors);
            }

            var block = new Block
            {
                Name = blockDto.Name,
                //UnitManagerId = response.Data.Id
            };

            await _context.Blocks.AddAsync(block);
            if (await _context.SaveChangesAsync() > 0)
            {
                // Refactor and improve performance
                var retrunBlock = new RetrunBlockDto
                {
                    Id = block.Id,
                    Name = blockDto.Name,
                    //PersonId = blockDto.PersonId,
                    ManagerId = response.Data.Id,
                    Role = response.Data.Role,
                    Email = response.Data.Email,
                    FullName = person.FullName
                };

                _logger.LogInformation("Successfully added block '{BlockName}' with ID {BlockId}", block.Name, block.Id);
                return ApiResponse<RetrunBlockDto>.Success(
                    retrunBlock,
                    "تمت إضافة البلوك بنجاح. تم إرسال رمز التأكيد إلى البريد الإلكتروني."
                );
            }

            _logger.LogError("Failed to create block manager: {Error}", response.Message);
            return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.BadRequest, "فشل في إضافة البلوك.");
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
        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialUnitDto blockDto)
        {
            _logger.LogInformation("Attempting to update block with ID: {BlockId}", id);

            var existingBlock = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlock == null)
            {
                _logger.LogWarning("Block with ID {BlockId} not found", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المربع غير موجود.");
            }

            existingBlock.Name = blockDto.Name;
            _context.Blocks.Update(existingBlock);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("Block ID {BlockId} name updated to '{NewName}'", id, blockDto.Name);
                return ApiResponse<string>.Success(message: "تم تحديث اسم المربع بنجاح.");
            }

            _logger.LogError("Failed to update block with ID {BlockId}", id);
            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "فشل في تحديث المربع.");
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
