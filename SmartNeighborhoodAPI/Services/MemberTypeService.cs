using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class MemberFamilyRoleService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;


        public MemberFamilyRoleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<MemberFamilyRole>> AddAsync(string nameMemberFamilyRole)
        {


            var MemberFamilyRole = new MemberFamilyRole
            {
              
                RoleName = nameMemberFamilyRole

            };


            var existMemberFamilyRole = await _context.MemberFamilyRoles.FirstOrDefaultAsync(x => x.RoleName== nameMemberFamilyRole);
            if (existMemberFamilyRole != null)
                return ApiResponse<MemberFamilyRole>.Error(HttpStatusCode.Conflict, "MemberFamilyRoles Is Already Exist");


            await _context.MemberFamilyRoles.AddAsync(MemberFamilyRole);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<MemberFamilyRole>.Success(MemberFamilyRole, "Added Successed");

            return ApiResponse<MemberFamilyRole>.Error(HttpStatusCode.BadRequest, "Block not add");


        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.MemberFamilyRoles.FindAsync(id);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Block Not Found");

            _context.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Block Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the Block");
        }
        public async Task<ApiResponse<IEnumerable<MemberFamilyRoleDto>>> GetAll()
        {
            var MemberFamilyRoles = _context.MemberFamilyRoles.AsNoTracking().ToList();
            if (MemberFamilyRoles.Count > 0)
            {
                var MemberFamilyRoleDtos = MemberFamilyRoles.Select( x =>  new MemberFamilyRoleDto
                {
                    Id = x.Id,
                    RoleName = x.RoleName,
                }).ToList();
                return ApiResponse<IEnumerable<MemberFamilyRoleDto>>.Success(MemberFamilyRoleDtos);
            }

            return ApiResponse<IEnumerable<MemberFamilyRoleDto>>.Error(HttpStatusCode.NotFound, "No Block Found");



        }
        public async Task<ApiResponse<MemberFamilyRoleDto>> GetByIdAsync(int id)
        {
            var MemberFamilyRole = await _context.MemberFamilyRoles.Include(x=>x.FamilyMembers).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (MemberFamilyRole == null)
                return ApiResponse<MemberFamilyRoleDto>.Error(HttpStatusCode.NotFound, "Block Not Found");


            var MemberFamilyRoleDto = new MemberFamilyRoleDto
            {
                Id = id,
                RoleName = MemberFamilyRole.RoleName           
            };
            return ApiResponse<MemberFamilyRoleDto>.Success(MemberFamilyRoleDto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, string newNameMemberFamilyRole)
        {
            var exisxtMemberFamilyRole = await _context.MemberFamilyRoles.FirstOrDefaultAsync(x => x.Id == id);

            if (exisxtMemberFamilyRole is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Block Not Found");



            if (exisxtMemberFamilyRole.RoleName == newNameMemberFamilyRole)
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "MemberFamilyRoles Is Already Exist");


            
            exisxtMemberFamilyRole.RoleName = newNameMemberFamilyRole;
           

            _context.MemberFamilyRoles.Update(exisxtMemberFamilyRole);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Block Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update Block");


        }
    }
}
