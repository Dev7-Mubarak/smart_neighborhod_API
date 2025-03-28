using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class BlockServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;


        public BlockServices(ApplicationDbContext context, IMapper mapper, IAuthService authService, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<ApiResponse<PaginatedResult<Family>>> GetBlockFamiliesById(int blockId, int pageNumber, int pageSize, string? search)
        {
            if (await _context.Blocks.FindAsync(blockId) is null)
                return ApiResponse<PaginatedResult<Family>>.Error(HttpStatusCode.NotFound, "Block Not Found");

            var query = _context.Families.AsNoTracking().AsQueryable();

            if (string.IsNullOrEmpty(search))
            {
                return ApiResponse<PaginatedResult<Family>>.Success(await query.Where(x => x.BlockId == blockId).ToPaginatedListAsync(pageNumber, pageSize));
            }

            return ApiResponse<PaginatedResult<Family>>.Success(await query.Where(x => x.BlockId == blockId).Where(x => x.Name.Contains(search)).ToPaginatedListAsync(pageNumber, pageSize));
        }

        public async Task<ApiResponse<Block>> AddAsync(BlockDto blockDto)
        {

            var existblock = await _context.Blocks.FirstOrDefaultAsync(x => x.Name == blockDto.Name);
            if (existblock != null)
                return ApiResponse<Block>.Error(HttpStatusCode.Conflict, "Block Name Is Already Exist");

            if (await _userManager.FindByEmailAsync(blockDto.Email) is not null)
                return ApiResponse<Block>.Error(HttpStatusCode.Conflict, "User with this email already exists.");

            RegisterDto registerDto = new RegisterDto
            {
                Email = blockDto.Email,
                Password = blockDto.Password
            };

            var user = await _authService.RegisterAsync(registerDto);

            var block = new Block
            {
                Name = blockDto.Name,
                ManagerId = user.Data.Id
            };

            await _context.Blocks.AddAsync(block);
            if (await _context.SaveChangesAsync() > 0)
            {
                return ApiResponse<Block>.Success(block, "Added Successfully");
            }

            return ApiResponse<Block>.Error(HttpStatusCode.BadRequest, "Block not added");
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

        public async Task<ApiResponse<PaginatedResult<Block>>> GetAll(int pageNumber, int pageSize, string? search)
        {
            var query = _context.Blocks.AsNoTracking().AsQueryable();

            if (string.IsNullOrEmpty(search))
            {
                return ApiResponse<PaginatedResult<Block>>.Success(await query.ToPaginatedListAsync(pageNumber, pageSize));
            }

            return ApiResponse<PaginatedResult<Block>>.Success(await query.Where(x => x.Name.Contains(search)).ToPaginatedListAsync(pageNumber, pageSize));
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
            //existingBlock.ManagerId = blockDto.UserId;

            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Block Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Failed To Update Block");
        }
    }
}
