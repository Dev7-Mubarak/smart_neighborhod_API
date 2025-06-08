using Microsoft.AspNetCore.Identity;
using OurProjectSmartNeiborhood.Services;
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


        public BlockServices(ApplicationDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<ApiResponse<IEnumerable<RetrunBlockDto>>> GetAllAsync()
        {
            var blocks = await _context.Blocks.Select(x => new RetrunBlockDto
            {
                Id = x.Id,
                ManagerId = x.ManagerId,
                Name = x.Name,
                Email = x.Manager.Email,
                PersonId = x.Manager.Person.Id,
                FullName = x.Manager.Person.FullName
            }).AsNoTracking().ToListAsync();

            return ApiResponse<IEnumerable<RetrunBlockDto>>.Success(blocks);
        }
        public async Task<ApiResponse<RetrunBlockDto>> AddAsync(BlockDto blockDto)
        {

            var existblock = await _context.Blocks.FirstOrDefaultAsync(x => x.Name == blockDto.Name);
            if (existblock != null)
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "Block Name Is Already Exist");

            var person = await _context.People.FindAsync(blockDto.PersonId);
            if (person == null)
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "Person Not Found");

            CreateBlockManagerDto blockManagerDto = new CreateBlockManagerDto
            {
                Email = blockDto.Email,
                PersonId = blockDto.PersonId,
                Password = blockDto.Password
            };

            var response = await _authService.CreateBlockManagerAsync(blockManagerDto);

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

                return ApiResponse<RetrunBlockDto>.Success(retrunBlock, "User added successfully. Confirmation code delivered via email.");
            }

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
