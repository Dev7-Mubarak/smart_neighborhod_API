//using System.Net;

//namespace SmartNeighborhoodAPI.Services
//{
//    public class TeamsService
//    {
//        private readonly ApplicationDbContext _context;
//        readonly IMapper _mapper;

//        public TeamsService(ApplicationDbContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<ApiResponse<TeamDto>> AddAsync(TeamDto TeamDto)
//        {

//            var team = _mapper.Map<Team>(TeamDto);


//            await _context.Teams.AddAsync(team);
//            if (await _context.SaveChangesAsync() > 0)
//                return ApiResponse<TeamDto>.Success(TeamDto, "Added Successed");

//            return ApiResponse<TeamDto>.Error(HttpStatusCode.BadRequest, "team not add");


//        }
//        public async Task<ApiResponse<string>> DeleteAsync(int id)
//        {
//            var entity = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id); ;
//            if (entity == null)
//                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "team Not Found");

//            _context.Teams.Remove(entity);
//            if (await _context.SaveChangesAsync() > 0)
//                return ApiResponse<string>.Success("team Deleted Successfully");

//            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the team");
//        }
//        public async Task<ApiResponse<IEnumerable<TeamDto>>> GetAll()
//        {
//            var teams = await _context.Teams.AsNoTracking().ToListAsync();
//            if (teams.Count > 0)
//            {
//                var TeamDtos = _mapper.Map<IEnumerable<TeamDto>>(teams);
//                return ApiResponse<IEnumerable<TeamDto>>.Success(TeamDtos);
//            }

//            return ApiResponse<IEnumerable<TeamDto>>.Error(HttpStatusCode.NotFound, "No teams Found");



//        }
//        public async Task<ApiResponse<TeamDto>> GetByIdAsync(int id)
//        {
//            var team = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
//            if (team == null)
//                return ApiResponse<TeamDto>.Error(HttpStatusCode.NotFound, "team Not Found");


//            var TeamDto = _mapper.Map<TeamDto>(team);
//            return ApiResponse<TeamDto>.Success(TeamDto);
//        }
//        public async Task<ApiResponse<string>> UpdateAsync(int id, TeamDto TeamDto)
//        {
//            var ExsitTeam = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

//            if (ExsitTeam is null)
//                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Team Not Found");
//            var UpdateTeam = _mapper.Map(TeamDto, ExsitTeam);

//            _context.Teams.Update(UpdateTeam);
//            if (await _context.SaveChangesAsync() > 0)
//                return ApiResponse<string>.Success("Team Updated Successfully");

//            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update Team");


//        }
//    }
//}
