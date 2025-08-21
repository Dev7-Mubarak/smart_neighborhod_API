using System.Net;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services
{

    public class TeamMemberService : ITeamMemberService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;
        private readonly ILogger<TeamMember> _logger;

        public TeamMemberService(ApplicationDbContext context, IMapper mapper, ILogger<TeamMember> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<string>> AddAsync(AddTeamMemberDto dto)
        {
            _logger.LogInformation("Adding member {PersonId} to team {TeamId}", dto.PersonId, dto.TeamId);

            var teamExists = await _context.Teams.AnyAsync(t => t.Id == dto.TeamId);
            if (!teamExists)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الفريق");

            var personExists = await _context.People.AnyAsync(p => p.Id == dto.PersonId);
            if (!personExists)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الشخص");

            var role = await _context.TeamRoles.FirstOrDefaultAsync(r => r.Id == dto.TeamRoleId);
            if (role == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الدور");

            var alreadyMember = await _context.TeamMembers
                .AnyAsync(m => m.TeamId == dto.TeamId && m.PersonId == dto.PersonId);
            if (alreadyMember)
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "الشخص عضو بالفعل في الفريق");

            if (role.Name == "مدير المشروع")
            {
                var hasManager = await _context.TeamMembers
                    .Include(m => m.TeamRole)
                    .AnyAsync(m => m.TeamId == dto.TeamId && m.TeamRole.Name == "مدير المشروع");

                if (hasManager)
                    return ApiResponse<string>.Error(HttpStatusCode.Conflict, "لا يمكن إضافة أكثر من مدير مشروع واحد إلى نفس الفريق");
            }

            var teamMember = new TeamMember
            {
                TeamId = dto.TeamId,
                PersonId = dto.PersonId,
                TeamRoleId = dto.TeamRoleId,
                DateOfJoin = dto.DateOfJoin
            };

            await _context.TeamMembers.AddAsync(teamMember);
            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("Team member added successfully.");
                return ApiResponse<string>.Success("تمت إضافة العضو إلى الفريق بنجاح");
            }

            _logger.LogError("Failed to add team member.");
            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "فشل في إضافة العضو");
        }


        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete TeamMember with ID {TeamMemberId}", id);

            var entity = await _context.TeamMembers.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("TeamMember with ID {TeamMemberId} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على عضو الفريق");
            }

            _context.TeamMembers.Remove(entity);

            var result = await _context.SaveChangesAsync();

            if (result <= 0)
            {
                _logger.LogError("Failed to delete TeamMember with ID {TeamMemberId}", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في حذف عضو الفريق");
            }

            _logger.LogInformation("TeamMember with ID {TeamMemberId} deleted successfully.", id);
            return ApiResponse<string>.Success("تم حذف عضو الفريق بنجاح");
        }

        public async Task<ApiResponse<IEnumerable<TeamMemberDetailsDto>>> GetAll()
        {
            _logger.LogInformation("Fetching all team members with related data...");

            var teamMembers = await _context.TeamMembers
                .Include(tm => tm.Person)
                .Include(tm => tm.Team)
                .Include(tm => tm.TeamRole)
                .AsNoTracking()
                .ToListAsync();

            if (!teamMembers.Any())
            {
                _logger.LogWarning("No team members found.");
                return ApiResponse<IEnumerable<TeamMemberDetailsDto>>.Error(HttpStatusCode.NotFound, "لا يوجد أعضاء في الفريق");
            }

            var dtos = teamMembers.Select(tm => new TeamMemberDetailsDto
            {
                TeamMemberId = tm.Id,
                PersonId = tm.PersonId,
                PersonName = tm.Person?.FullName ?? "غير معروف",
                TeamId = tm.TeamId,
                TeamName = tm.Team?.Name ?? "غير معروف",
                DateOfJoin = tm.DateOfJoin,
                TeamRoleId = tm.TeamRoleId,
                TeamRoleName = tm.TeamRole?.Name ?? "غير معروف"
            }).ToList();

            _logger.LogInformation("{Count} team members retrieved and mapped.", dtos.Count);

            return ApiResponse<IEnumerable<TeamMemberDetailsDto>>.Success(dtos);
        }

        public async Task<ApiResponse<TeamMemberDto>> GetByIdAsync(int id)
        {
            var TeamMember = await _context.TeamMembers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (TeamMember == null)
                return ApiResponse<TeamMemberDto>.Error(HttpStatusCode.NotFound, "TeamMember Not Found");


            var TeamMemberDto = _mapper.Map<TeamMemberDto>(TeamMember);
            return ApiResponse<TeamMemberDto>.Success(TeamMemberDto);
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateTeamMemberDto dto)
        {
            _logger.LogInformation("Updating team member with ID {TeamMemberId}", id);

            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على العضو في الفريق");

            var teamExists = await _context.Teams.AnyAsync(t => t.Id == dto.TeamId);
            if (!teamExists)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الفريق");

            var roleExists = await _context.TeamRoles.AnyAsync(r => r.Id == dto.TeamRoleId);
            if (!roleExists)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الدور");

            var personId = teamMember.PersonId;

            // Optional: restrict "مدير المشروع" to only one per team
            if (dto.TeamRoleId == 1)
            {
                var anotherManagerExists = await _context.TeamMembers
                    .AnyAsync(m => m.Id != id && m.TeamId == dto.TeamId && m.TeamRoleId == 1);
                if (anotherManagerExists)
                    return ApiResponse<string>.Error(HttpStatusCode.Conflict, "لا يمكن تعيين أكثر من مدير مشروع واحد في الفريق");
            }

            // Update fields
            teamMember.TeamId = dto.TeamId;
            teamMember.TeamRoleId = dto.TeamRoleId;
            teamMember.DateOfJoin = dto.DateOfJoin;

            _context.TeamMembers.Update(teamMember);
            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("Team member with ID {TeamMemberId} updated successfully.", id);
                return ApiResponse<string>.Success("تم تحديث العضو في الفريق بنجاح");
            }

            _logger.LogError("Failed to update team member with ID {TeamMemberId}", id);
            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "فشل في تحديث العضو");
        }

    }


}
