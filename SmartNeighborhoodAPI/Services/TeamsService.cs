using System.Net;
using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Helpers.DTOs.Teams;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services
{
    public class TeamsService : ITeamsService
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

            var entity = await _context.Teams.Include(x => x.TeamMembers).FirstOrDefaultAsync(x => x.Id == id);
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
                .AsNoTracking()
                .Select(t => new CustomTeamDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    TeamMembers = t.TeamMembers.Select(tm => new TeamMemberDetailsDto
                    {
                        TeamMemberId = tm.Id,
                        PersonId = tm.PersonId,
                        PersonName = tm.Person.FullName,
                        TeamId = tm.TeamId,
                        TeamName = tm.Team.Name,
                        DateOfJoin = tm.DateOfJoin,
                        TeamRoleId = tm.TeamRoleId,
                        TeamRoleName = tm.TeamRole.Name
                    }).ToList()
                })
                .ToListAsync();

            if (!teams.Any())
            {
                _logger.LogWarning("No teams found in the database.");
                return ApiResponse<IEnumerable<CustomTeamDto>>.Error(HttpStatusCode.NotFound, "لا توجد فرق مسجلة");
            }

            _logger.LogInformation("{Count} teams retrieved from the database.", teams.Count);
            _logger.LogInformation("Successfully mapped teams to CustomTeamDto objects.");

            return ApiResponse<IEnumerable<CustomTeamDto>>.Success(teams, "تم جلب الفرق بنجاح");
        }
        public async Task<ApiResponse<TeamDto>> GetByIdAsync(int id)
        {
            var team = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (team == null)
                return ApiResponse<TeamDto>.Error(HttpStatusCode.NotFound, "team Not Found");


            var TeamDto = _mapper.Map<TeamDto>(team);
            return ApiResponse<TeamDto>.Success(TeamDto, "تم جلب الفريق بنجاح");
        }
        public async Task<ApiResponse<TeamDto>> UpdateAsync(int teamId, TeamDto dto)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null)
            {
                _logger.LogWarning("Team with ID {TeamId} not found", teamId);
                return ApiResponse<TeamDto>.Error(HttpStatusCode.NotFound, "الفريق غير موجود");
            }

            var isPersonExists = await _context.People.AnyAsync(x => x.Id == dto.TeamLeadId);
            if (!isPersonExists)
            {
                _logger.LogWarning("Team Lead with ID {TeamLeadId} not found", dto.TeamLeadId);
                return ApiResponse<TeamDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على قائد الفريق");
            }

            var isTeamNameExists = await _context.Teams
                .AnyAsync(t => t.Name.ToLower() == dto.Name.Trim().ToLower() && t.Id != teamId);

            if (isTeamNameExists)
            {
                _logger.LogWarning("Team name '{TeamName}' already exists", dto.Name);
                return ApiResponse<TeamDto>.Error(HttpStatusCode.Conflict, "اسم الفريق مستخدم من قبل");
            }

            // Update team info
            team.Name = dto.Name;

            // Check if the team already has a team lead, remove or update if needed
            var currentTeamLead = await _context.TeamMembers
                .FirstOrDefaultAsync(tm => tm.TeamId == teamId && tm.TeamRole.Name == "مدير المشروع");

            if (currentTeamLead != null)
            {
                // Update existing team lead
                currentTeamLead.PersonId = dto.TeamLeadId;
                currentTeamLead.DateOfJoin = dto.InJoiedDate;
            }
            else
            {
                // Add new team lead
                var teamLeadRole = await _context.TeamRoles.FirstOrDefaultAsync(x => x.Name == "مدير المشروع");
                if (teamLeadRole == null)
                {
                    _logger.LogError("Team role 'مدير المشروع' not found");
                    return ApiResponse<TeamDto>.Error(HttpStatusCode.NotFound, "الدور 'مدير المشروع' غير موجود");
                }

                var teamLead = new TeamMember
                {
                    TeamId = teamId,
                    PersonId = dto.TeamLeadId,
                    DateOfJoin = dto.InJoiedDate,
                    TeamRoleId = teamLeadRole.Id
                };

                await _context.TeamMembers.AddAsync(teamLead);
            }

            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to update team with ID {TeamId}", teamId);
                return ApiResponse<TeamDto>.Error(HttpStatusCode.BadRequest, "فشل في تحديث الفريق");
            }

            _logger.LogInformation("Team with ID {TeamId} updated successfully", teamId);
            return ApiResponse<TeamDto>.Success(dto, "تم تحديث الفريق بنجاح");
        }
        public async Task<ApiResponse<IEnumerable<ReturnProjectDto>>> GetTeamProjects(int teamId)
        {
            _logger.LogInformation("Fetching projects assigned to team with ID {TeamId}.", teamId);

            var projects = await _context.ProjectTeams
                .Where(pt => pt.TeamId == teamId)
                .Include(pt => pt.Project)
                    //.ThenInclude(p => p.Manager)
                .Include(pt => pt.Project)
                    .ThenInclude(p => p.ProjectCatogory)
                .Select(pt => pt.Project)
                .AsNoTracking()
                .ToListAsync();


            if (projects.Count == 0)
            {
                _logger.LogWarning("No projects found for team with ID {TeamId}.", teamId);
                return ApiResponse<IEnumerable<ReturnProjectDto>>.Error(HttpStatusCode.NotFound, "لا توجد مشاريع لهذا الفريق");
            }

            var projectDtos = projects.Select(project => new ReturnProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                ProjectStatus = GetDisplayName(project.ProjectStatus),
                ProjectPriority = GetDisplayName(project.ProjectPriority),
                Budget = project.Budget,
                //Manager = new CustomPersonDto
                //{
                //    Id = project.Manager.Id,
                //    FullName = project.Manager.FullName
                //},
                ProjectCatgory = project.ProjectCatogory
            }).ToList();

            return ApiResponse<IEnumerable<ReturnProjectDto>>.Success(projectDtos);
        }

        private static string GetDisplayName<T>(T enumValue)
        {
            var memberInfo = typeof(T).GetMember(enumValue.ToString()).FirstOrDefault();
            var displayAttr = memberInfo?.GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .FirstOrDefault() as DisplayAttribute;

            return displayAttr?.Name ?? enumValue.ToString();
        }

    }
}
