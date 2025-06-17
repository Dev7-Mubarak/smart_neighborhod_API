using SmartNeighborhoodAPI.Helpers.DTOs;
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
            public async Task<ApiResponse<TeamWithMembersDto>> AddAsync(TeamWithMembersDto teamWithMemberDto)
            {

                var TeamMember = _mapper.Map<TeamMember>(teamWithMemberDto);


                await _context.AddAsync(TeamMember);
                if (await _context.SaveChangesAsync() > 0)
                    return ApiResponse<TeamWithMembersDto>.Success(teamWithMemberDto, "Added Successed");

                return ApiResponse<TeamWithMembersDto>.Error(HttpStatusCode.BadRequest, "TeamMember not add");


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
            public async Task<ApiResponse<IEnumerable<TeamWithMembersDto>>> GetAll()
            {
                var TeamMembers = await _context.TeamMembers.AsNoTracking().ToListAsync();
                if (TeamMembers.Count > 0)
                {
                    var TeamWithMemberDtos = _mapper.Map<IEnumerable<TeamWithMembersDto>>(TeamMembers);
                    return ApiResponse<IEnumerable<TeamWithMembersDto>>.Success(TeamWithMemberDtos);
                }

                return ApiResponse<IEnumerable<TeamWithMembersDto>>.Error(HttpStatusCode.NotFound, "No TeamMembers Found");



            }
            public async Task<ApiResponse<TeamWithMembersDto>> GetByIdAsync(int id)
            {
                var TeamMember = await _context.TeamMembers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (TeamMember == null)
                    return ApiResponse<TeamWithMembersDto>.Error(HttpStatusCode.NotFound, "TeamMember Not Found");


                var TeamWithMemberDto = _mapper.Map<TeamWithMembersDto>(TeamMember);
                return ApiResponse<TeamWithMembersDto>.Success(TeamWithMemberDto);
            }
            public async Task<ApiResponse<string>> UpdateAsync(int id, TeamWithMembersDto TeamWithMemberDto)
            {
                var ExsitTeamMember = await _context.TeamMembers.FirstOrDefaultAsync(x => x.Id == id);

                if (ExsitTeamMember is null)
                    return ApiResponse<string>.Error(HttpStatusCode.NotFound, "TeamMember Not Found");
                var UpdateTeamMember = _mapper.Map(TeamWithMemberDto, ExsitTeamMember);

                _context.TeamMembers.Update(UpdateTeamMember);
                if (await _context.SaveChangesAsync() > 0)
                    return ApiResponse<string>.Success("TeamMember Updated Successfully");

                return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update TeamMember");


            }
        }
    

}
