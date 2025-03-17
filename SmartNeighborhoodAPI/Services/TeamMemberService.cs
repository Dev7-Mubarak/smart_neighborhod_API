using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    
        public class TeamMemberService
        {
            private readonly ApplicationDbContext _context;
            readonly IMapper _mapper;

            public TeamMemberService(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ApiResponse<TeamMemberDto>> AddAsync(TeamMemberDto TeamMemberDto)
            {

                var TeamMember = _mapper.Map<TeamMember>(TeamMemberDto);


                await _context.AddAsync(TeamMember);
                if (await _context.SaveChangesAsync() > 0)
                    return ApiResponse<TeamMemberDto>.Success(TeamMemberDto, "Added Successed");

                return ApiResponse<TeamMemberDto>.Error(HttpStatusCode.BadRequest, "TeamMember not add");


            }
            public async Task<ApiResponse<string>> DeleteAsync(int id)
            {
                var entity = await _context.TeamMembers.FirstOrDefaultAsync(x => x.Id == id); ;
                if (entity == null)
                    return ApiResponse<string>.Error(HttpStatusCode.NotFound, "TeamMember Not Found");

                _context.TeamMembers.Remove(entity);
                if (await _context.SaveChangesAsync() > 0)
                    return ApiResponse<string>.Success("TeamMember Deleted Successfully");

                return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the TeamMember");
            }
            public async Task<ApiResponse<IEnumerable<TeamMemberDto>>> GetAll()
            {
                var TeamMembers = await _context.TeamMembers.AsNoTracking().ToListAsync();
                if (TeamMembers.Count > 0)
                {
                    var TeamMemberDtos = _mapper.Map<IEnumerable<TeamMemberDto>>(TeamMembers);
                    return ApiResponse<IEnumerable<TeamMemberDto>>.Success(TeamMemberDtos);
                }

                return ApiResponse<IEnumerable<TeamMemberDto>>.Error(HttpStatusCode.NotFound, "No TeamMembers Found");



            }
            public async Task<ApiResponse<TeamMemberDto>> GetByIdAsync(int id)
            {
                var TeamMember = await _context.TeamMembers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (TeamMember == null)
                    return ApiResponse<TeamMemberDto>.Error(HttpStatusCode.NotFound, "TeamMember Not Found");


                var TeamMemberDto = _mapper.Map<TeamMemberDto>(TeamMember);
                return ApiResponse<TeamMemberDto>.Success(TeamMemberDto);
            }
            public async Task<ApiResponse<string>> UpdateAsync(int id, TeamMemberDto TeamMemberDto)
            {
                var ExsitTeamMember = await _context.TeamMembers.FirstOrDefaultAsync(x => x.Id == id);

                if (ExsitTeamMember is null)
                    return ApiResponse<string>.Error(HttpStatusCode.NotFound, "TeamMember Not Found");
                var UpdateTeamMember = _mapper.Map(TeamMemberDto, ExsitTeamMember);

                _context.TeamMembers.Update(UpdateTeamMember);
                if (await _context.SaveChangesAsync() > 0)
                    return ApiResponse<string>.Success("TeamMember Updated Successfully");

                return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update TeamMember");


            }
        }
    

}
