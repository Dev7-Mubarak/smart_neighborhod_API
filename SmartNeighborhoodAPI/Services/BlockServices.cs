using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Interfaces;
using System.Data;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class BlockServices
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
            var blocks = await _context.Blocks.Select(x => new RetrunBlockDto
            {
                Id = x.Id,
                ManagerId = x.ManagerId,
                Name = x.Name,
                Email = x.Manager.Email,
                PersonId = x.Manager.Person.Id,
                FullName = x.Manager.Person.FullName
            }).AsNoTracking().ToListAsync();


            _logger.LogInformation("Fetched {Count} blocks", blocks.Count);

            return ApiResponse<IEnumerable<RetrunBlockDto>>.Success(blocks);
        }

        public async Task<ApiResponse<RetrunBlockDto>> ChangeBlockManager(ChangeBlockManagerDto blockManagerDto)
        {
            _logger.LogInformation("Initiating change of block manager for BlockId: {BlockId}, PersonId: {PersonId}",
                blockManagerDto.BlockId, blockManagerDto.PersonId);

            // Step 1: Validate block
            var block = await _context.Blocks.FindAsync(blockManagerDto.BlockId);
            if (block == null)
            {
                _logger.LogWarning("Block with ID '{BlockId}' not found.", blockManagerDto.BlockId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على مربع.");
            }

            // Step 2: Validate person
            var person = await _context.People.FindAsync(blockManagerDto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", blockManagerDto.PersonId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الشخص.");
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

            // Step 6: Delete old manager account (if any)
            var deleteResult = await _authService.DeleteBlockManagerAccountByIdAsync(oldManagerId);
            if (!deleteResult.IsSuccess)
            {
                _logger.LogError("Failed to delete old block manager with ID: {OldManagerId}", oldManagerId);
                return ApiResponse<RetrunBlockDto>.Error(deleteResult.StatusCode, deleteResult.Message, deleteResult.Errors);
            }

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
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "Block Name Is Already Exist");
            }

            var person = await _context.People.FindAsync(blockDto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", blockDto.PersonId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "Person Not Found");
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
                return ApiResponse<RetrunBlockDto>.Error(response.StatusCode, response.Message, response.Errors);
            }

            var block = new Block
            {
                Name = blockDto.Name,
                ManagerId = response.Data.Id
            };

            await _context.Blocks.AddAsync(block);
            if (await _context.SaveChangesAsync() > 0)
            {
                //Refacore and Improve Perfermance
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
                return ApiResponse<RetrunBlockDto>.Success(retrunBlock, "User added successfully. Confirmation code delivered via email.");
            }

            _logger.LogError("Failed to create block manager: {Error}", response.Message);
            return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.BadRequest, "Block not added");
        }
        public async Task<ApiResponse<Block>> GetByIdAsync(int id)
        {
            var block = await _context.Blocks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (block == null)
                return ApiResponse<Block>.Error(HttpStatusCode.NotFound, "Block Not Found");

            return ApiResponse<Block>.Success(block);
        }

             public async Task<ApiResponse<string>> UpdateAsync(int id, BlockDto blockDto)
        {
            var existingBlock = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlock == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Block Not Found");

            existingBlock.Name = blockDto.Name;
            //existingBlock.ManagerId = blockDto.PersonId;
            //existingBlock.ManagerId = blockDto.UserId;

            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Block Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Failed To Update Block");
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var block = await _context.Blocks.FindAsync(id);
            if (block == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Block Not Found");

            _context.Blocks.Remove(block);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Block Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Failed To Delete the Block");
        }


        public async Task<ApiResponse<PaginatedResult<Family>>> GetBlockFamiliesById(int blockId, int pageNumber, int pageSize, string? search)
        {
            if (await _context.Blocks.FindAsync(blockId) is null)
                return ApiResponse<PaginatedResult<Family>>.Error(HttpStatusCode.NotFound, "Block Not Found");

            var query = _context.Families.AsNoTracking();

            if (string.IsNullOrEmpty(search))
            {
                return ApiResponse<PaginatedResult<Family>>.Success(await query.Where(x => x.BlockId == blockId).ToPaginatedListAsync(pageNumber, pageSize));
            }

            return ApiResponse<PaginatedResult<Family>>.Success(await query.Where(x => x.BlockId == blockId).Where(x => x.Name.Contains(search)).ToPaginatedListAsync(pageNumber, pageSize));
        }
    }
}
