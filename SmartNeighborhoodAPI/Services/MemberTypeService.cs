using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class MemberTypeService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;


        public MemberTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<MemberType>> AddAsync(string nameMemberType)
        {


            var memberType = new MemberType
            {
              
                Name = nameMemberType

            };


            var existMemberType = await _context.MemberTypes.FirstOrDefaultAsync(x => x.Name == nameMemberType);
            if (existMemberType != null)
                return ApiResponse<MemberType>.Error(HttpStatusCode.Conflict, "MemberTypes Is Already Exist");


            await _context.MemberTypes.AddAsync(memberType);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<MemberType>.Success(memberType, "Added Successed");

            return ApiResponse<MemberType>.Error(HttpStatusCode.BadRequest, "Block not add");


        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.MemberTypes.FindAsync(id);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Block Not Found");

            _context.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Block Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the Block");
        }
        public async Task<ApiResponse<IEnumerable<MemberTypeDto>>> GetAll()
        {
            var memberTypes = _context.MemberTypes.AsNoTracking().ToList();
            if (memberTypes.Count > 0)
            {
                var MemberTypeDtos = memberTypes.Select( x =>  new MemberTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
                return ApiResponse<IEnumerable<MemberTypeDto>>.Success(MemberTypeDtos);
            }

            return ApiResponse<IEnumerable<MemberTypeDto>>.Error(HttpStatusCode.NotFound, "No Block Found");



        }
        public async Task<ApiResponse<MemberTypeDto>> GetByIdAsync(int id)
        {
            var memberType = await _context.MemberTypes.Include(x=>x.FamilyMembers).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (memberType == null)
                return ApiResponse<MemberTypeDto>.Error(HttpStatusCode.NotFound, "Block Not Found");


            var MemberTypeDto = new MemberTypeDto
            {
                Id = id,
                Name = memberType.Name
            };
            return ApiResponse<MemberTypeDto>.Success(MemberTypeDto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, string newNameMemberType)
        {
            var exisxtMemberType = await _context.MemberTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (exisxtMemberType is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Block Not Found");



            if (exisxtMemberType.Name == newNameMemberType)
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "MemberTypes Is Already Exist");


            
            exisxtMemberType.Name = newNameMemberType;
           

            _context.MemberTypes.Update(exisxtMemberType);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Block Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update Block");


        }
    }
}
