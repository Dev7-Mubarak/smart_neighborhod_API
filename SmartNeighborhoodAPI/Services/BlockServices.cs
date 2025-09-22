using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Interfaces;
using System.Data;
using System.Linq;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class BlockServices : IBlockServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ILogger<Block> _logger;
        private readonly UserManager<AppUser> _userManager;


        public BlockServices(ApplicationDbContext context, IMapper mapper, IAuthService authService, ILogger<Block> logger, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<ApiResponse<IEnumerable<RetrunBlockDto>>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all blocks");

            var blocks = await _context.Blocks
                .Select(x => new RetrunBlockDto
                {
                    Id = x.Id,
                    ManagerId = x.ManagerId,
                    Name = x.Name,
                    Email = x.Manager.Email,
                    PersonId = x.Manager.Person.Id,
                    FullName = x.Manager.Person.FullName
                })
                .AsNoTracking()
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} blocks", blocks.Count);

            string message = blocks.Any()
                ? "تم جلب جميع البيانات بنجاح."
                : "لا توجد بيانات متاحة.";

            return ApiResponse<IEnumerable<RetrunBlockDto>>.Success(blocks, message);
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

            // Step 3: Check if person is already a user
            var user = await _context.Users.FirstOrDefaultAsync(x => x.PersonId == person.Id);
            if (user != null)
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if (isAdmin)
                {
                    block.ManagerId = user.Id;
                    _context.Blocks.Update(block);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Block manager updated for existing admin. Block ID: {BlockId}", block.Id);

                    return ApiResponse<RetrunBlockDto>.Success(new RetrunBlockDto
                    {
                        Id = block.Id,
                        Name = block.Name,
                        ManagerId = user.Id,
                        PersonId = person.Id,
                        Email = user.Email,
                        FullName = person.FullName
                    }, "تم تحديث مدير المربع بنجاح.");
                }

                _logger.LogWarning("Person with ID '{PersonId}' is already a manager of another block.", person.Id);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "هذا الشخص مدير بالفعل مربع آخر.");
            }

            // Step 4: Create new manager account
            var createResult = await _authService.CreateBlockManagerAccountAsync(new CreateBlockManagerDto
            {
                Email = blockManagerDto.Email,
                Password = blockManagerDto.Password,
                PersonId = blockManagerDto.PersonId
            });

            if (!createResult.IsSuccess)
            {
                _logger.LogError("Failed to create new block manager. Reason: {Reason}", createResult.Message);
                return ApiResponse<RetrunBlockDto>.Error(createResult.StatusCode, createResult.Message, createResult.Errors);
            }

            
            var oldManagerId = block.ManagerId;

            using var transaction = await _context.Database.BeginTransactionAsync();

            // Step 5: Update block manager
            block.ManagerId = createResult.Data.Id;
            _context.Blocks.Update(block);
            await _context.SaveChangesAsync();

            //// Step 6: Delete old manager account (if any)
            //var deleteResult = await _authService.DeleteBlockManagerAccountByIdAsync(oldManagerId);
            //if (!deleteResult.IsSuccess)
            //{
            //    _logger.LogError("Failed to delete old block manager with ID: {OldManagerId}", oldManagerId);
            //    return ApiResponse<RetrunBlockDto>.Error(deleteResult.StatusCode, deleteResult.Message, deleteResult.Errors);
            //}

            await transaction.CommitAsync();

            // Step 7: Return success response
            var returnBlockDto = new RetrunBlockDto
            {
                Id = block.Id,
                Name = block.Name,
                ManagerId = block.ManagerId,
                PersonId = person.Id,
                Email = createResult.Data.Email,
                FullName = person.FullName
            };

            _logger.LogInformation("Block manager updated successfully for block '{BlockName}' (ID: {BlockId})",
                block.Name, block.Id);

            return ApiResponse<RetrunBlockDto>.Success(returnBlockDto,
                "تم تحديث مدير المربع بنجاح. تم إرسال بيانات تسجيل الدخول عبر البريد الإلكتروني.");
        }
        public async Task<ApiResponse<RetrunBlockDto>> AddAsync(BlockDto blockDto)
        {
            _logger.LogInformation("Attempting to add a new block with name: {BlockName}", blockDto.Name);

            var existblock = await _context.Blocks.FirstOrDefaultAsync(x => x.Name == blockDto.Name);
            if (existblock != null)
            {
                _logger.LogWarning("Block with name '{BlockName}' already exists", blockDto.Name);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "اسم البلوك موجود مسبقًا.");
            }

            var person = await _context.People.FindAsync(blockDto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", blockDto.PersonId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود.");
            }

            CreateBlockManagerDto blockManagerDto = new CreateBlockManagerDto
            {
                Email = blockDto.Email,
                PersonId = blockDto.PersonId,
                Password = blockDto.Password
            };

            var response = await _authService.CreateBlockManagerAccountAsync(blockManagerDto);

            if (!response.IsSuccess)
            {
                var resp = ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.BadRequest, response.Message, response.Errors);

                return resp;
            }

            var block = new Block
            {
                Name = blockDto.Name,
                ManagerId = response.Data.Id
            };

            await _context.Blocks.AddAsync(block);
            if (await _context.SaveChangesAsync() > 0)
            {
                // Refactor and improve performance
                var retrunBlock = new RetrunBlockDto
                {
                    Id = block.Id,
                    Name = blockDto.Name,
                    PersonId = blockDto.PersonId,
                    ManagerId = response.Data.Id,
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
            var block = await _context.Blocks.FindAsync(id);
            if (block == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المربع غير موجود.");

            _context.Blocks.Remove(block);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("تم حذف المربع بنجاح.");

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
                        ManagerName = x.Manager.Person.FullName,
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

    }
}
