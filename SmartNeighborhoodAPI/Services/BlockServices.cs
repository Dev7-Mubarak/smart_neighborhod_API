using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class BlockServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BlockServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Block>> AddAsync(BlockDto blockDto)
        {

            var existMemberType = await _context.Blocks.FirstOrDefaultAsync(x => x.Name == blockDto.Name);
            if (existMemberType != null)
                return ApiResponse<Block>.Error(HttpStatusCode.Conflict, "Block Name Is Already Exist");


            var block = new Block
            {
                Name = blockDto.Name,
                ManagerId = blockDto.UserId
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

        public async Task<ApiResponse<List<Block>>> GetAll()
        {
            return ApiResponse<List<Block>>.Success(await _context.Blocks.AsNoTracking().ToListAsync());
        }

        public async Task<ApiResponse<Block>> GetByIdAsync(int id)
        {
            var block = await _context.Blocks.Include(x=>x.Families).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
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
            existingBlock.ManagerId = blockDto.UserId;

            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Block Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Failed To Update Block");
        }
    }
}
