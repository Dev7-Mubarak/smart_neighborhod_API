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
        private readonly IManagerAccountService _managerAccountService;
        private readonly ILogger<Block> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserContextService _userContextService;


        public BlockServices(ApplicationDbContext context, IMapper mapper, IManagerAccountService managerAccountService, ILogger<Block> logger, UserManager<AppUser> userManager, UserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _managerAccountService = managerAccountService;
            _logger = logger;
            _userManager = userManager;
            _userContextService = userContextService;
        }

        public async Task<ApiResponse<IEnumerable<RetrunBlockDto>>> GetAllAsync()
        {
            var currentUser = _userContextService.GetCurrentUser();
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
                    Email = currentUser.Email,
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

            // Step 4: Create new manager account
            var createResult = await _managerAccountService.CreateManagerAccountAsync(
                blockManagerDto.PersonId,
                blockManagerDto.Email,
                blockManagerDto.Password,
                Role.BlockManager
            );

            if (!createResult.IsSuccess)
            {
                return ApiResponse<RetrunBlockDto>.Error(createResult.StatusCode, createResult.Message, createResult.Errors);
            }


            var oldManagerId = block.BlockManagerId;
            var user = createResult.Data;


            // Step 5: Update block manager
            //block.UnitManagerId = createResult.Data.Id;
            _context.Blocks.Update(block);
            await _context.SaveChangesAsync();

            if (user != null) { 
                // Step 6: Delete old manager account (if any)
                if (oldManagerId != null)
                {
                    var deleteResult = await _managerAccountService.DeleteManagerAccountAsync(oldManagerId);
                    if (!deleteResult.IsSuccess)
                    {
                        // Log warning or handle as needed, but manager is already changed
                         _logger.LogError("Failed to delete old block manager with ID: {OldManagerId}", oldManagerId);
                         // return ApiResponse<RetrunBlockDto>.Error(deleteResult.StatusCode, deleteResult.Message, deleteResult.Errors);
                    }
                }
            }


            // Step 7: Return success response
            var returnBlockDto = new RetrunBlockDto
            {
                Id = block.Id,
                Name = block.Name,
                //ManagerId = block.UnitManagerId,    
                PersonId = person.Id,
                Email = user.Email,
                Role = Role.BlockManager,
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
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "اسم المربع موجود مسبقًا.");
            }

            var person = await _context.People.FindAsync(blockDto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", blockDto.PersonId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود.");
            }
            
            var response = await _managerAccountService.CreateManagerAccountAsync(
                blockDto.PersonId,
                blockDto.Email,
                blockDto.Password,
                Role.BlockManager
            );

            if (!response.IsSuccess)
            {
                return ApiResponse<RetrunBlockDto>.Error(response.StatusCode, response.Message, response.Errors);
            }

            var user = response.Data;
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
                    PersonId = blockDto.PersonId,
                    ManagerId = user.Id,
                    Role = Role.BlockManager,
                    Email = user.Email,
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
            var block = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == id);
            if (block == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المربع غير موجود.");

             // Note: The commented out code referenced UnitManagerId, but here we might mean BlockManagerId.
             // Assuming Block has BlockManagerId based on context or we need to access it correctly.
             // If Block entity structure is unclear, I will assume it's BlockManagerId or similar from context.
             // Wait, I see `block.BlockManagerId` used in ChangeManager.
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
            
            // Re-enabling deletion logic using manager service if appropriate? 
            // The original code had it commented out. If the requirement is to "Extract all duplicated Identity logic",
            // and `DeleteAsync` had commented out logic, maybe I should leave it alone or fix it if requested.
            // Requirement says "Refactor BlockServices... Apply the same refactoring where applicable".
            // Since it was commented out, I'll leave it as is to avoid changing behavior, but note that if it were active, I'd use _managerAccountService.deleteManagerAccountAsync.

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

    }
}
