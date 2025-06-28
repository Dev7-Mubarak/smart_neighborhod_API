using System.Net;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Helpers.DTOs.Teams;

namespace SmartNeighborhoodAPI.Services
{
    public class TeamsService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;
        private readonly ILogger<TeamsService> _logger;

        public TeamsService(ApplicationDbContext context, IMapper mapper, ILogger<TeamsService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<TeamDto>> AddAsync(TeamDto dto)
        {
            var isPersonExists = _context.People.Any(x => x.Id == dto.TeamLeadId);
            if (!isPersonExists)
            {
                _logger.LogWarning("Team Lead with ID {TeamLeadId} not found", dto.TeamLeadId);
                return ApiResponse<TeamDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على قائد الفريق");
            }

            var isTeamNameExists = _context.Teams
                .Any(t => t.Name.ToLower() == dto.Name.Trim().ToLower());

            if (isTeamNameExists)
            {
                _logger.LogWarning("Team name '{TeamName}' already exists", dto.Name);
                return ApiResponse<TeamDto>.Error(HttpStatusCode.Conflict, "اسم الفريق مستخدم من قبل");
            }

            var team = new Team
            {
                Name = dto.Name,
            };

            await _context.AddAsync(team);

            var teamLeadRole = _context.TeamRoles.FirstOrDefault(x => x.Name == "مدير المشروع")
                ?? throw new InvalidOperationException("الدور 'مدير المشروع' غير موجود.");

            var teamMember = new TeamMember
            {
                PersonId = dto.TeamLeadId,
                Team = team,
                DateOfJoin = dto.InJoiedDate,
                TeamRoleId = teamLeadRole.Id
            };

            await _context.TeamMembers.AddAsync(teamMember);

            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to save team to the database");
                return ApiResponse<TeamDto>.Error(HttpStatusCode.BadRequest, "فشل في إضافة الفريق");
            }

            _logger.LogInformation("Team with ID {TeamId} added successfully", team.Id);
            return ApiResponse<TeamDto>.Success(dto, "تمت الإضافة بنجاح");
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete team with ID {TeamId}", id);

            var entity = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("Team with ID {TeamId} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الفريق");
            }

            _context.Teams.Remove(entity);

            if (await _context.SaveChangesAsync() <= 0)
            {

                _logger.LogError("Failed to delete team with ID {TeamId}", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في حذف الفريق");
            }

            _logger.LogInformation("Team with ID {TeamId} deleted successfully.", id);
            return ApiResponse<string>.Success("تم حذف الفريق بنجاح");
        }
        public async Task<ApiResponse<IEnumerable<CustomTeamDto>>> GetAll()
        {
            _logger.LogInformation("Getting all teams from the database...");

            var teams = await _context.Teams
                .Include(t => t.TeamMembers)
                    .ThenInclude(tm => tm.TeamRole) // Include TeamRole for each member
                .AsNoTracking()
                .ToListAsync();

            if (!teams.Any())
            {
                _logger.LogWarning("No teams found in the database.");
                return ApiResponse<IEnumerable<CustomTeamDto>>.Error(HttpStatusCode.NotFound, "لا توجد فرق مسجلة");
            }

            _logger.LogInformation("{Count} teams retrieved from the database.", teams.Count);

            var customTeamDtos = teams.Select(t => new CustomTeamDto
            {
                Id = t.Id,
                Name = t.Name,
                Members = t.TeamMembers.Select(m => new CustomTeamMemberDto
                {
                    Id = m.Id,
                    PersonId = m.PersonId,
                    DateOfJoin = m.DateOfJoin,
                    TeamRoleId = m.TeamRoleId,
                    TeamRoleName = m.TeamRole.Name ?? "غير معروف"
                }).ToList()
            }).ToList();

            _logger.LogInformation("Successfully mapped teams to CustomTeamDto objects.");

            return ApiResponse<IEnumerable<CustomTeamDto>>.Success(customTeamDtos, "تم جلب الفرق بنجاح");
        }
        public async Task<ApiResponse<TeamDto>> GetByIdAsync(int id)
        {
            //    var team = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            //    if (team == null)
            //        return ApiResponse<TeamDto>.Error(HttpStatusCode.NotFound, "team Not Found");


            //    var TeamDto = _mapper.Map<TeamDto>(team);
            return ApiResponse<TeamDto>.Success();
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, TeamDto TeamDto)
        {
            var ExsitTeam = await _context.Ads.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitTeam is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Team Not Found");
            var UpdateTeam = _mapper.Map(TeamDto, ExsitTeam);

            _context.Ads.Update(UpdateTeam);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Team Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update Team");


        }
    }
}
