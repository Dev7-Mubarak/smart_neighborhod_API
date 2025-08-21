using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Helpers.DTOs.Teams;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class TeamRoleService : ITeamRoleService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;
        private readonly ILogger<TeamMember> _logger;

        public TeamRoleService(ApplicationDbContext context, IMapper mapper, ILogger<TeamMember> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<TeamRole>>> GetAllAsync()
        {
            _logger.LogInformation("Getting all team roles from the database...");

            var teamRoles = await _context.TeamRoles.AsNoTracking()
                .ToListAsync();

            if (!teamRoles.Any())
            {
                _logger.LogWarning("No team roles found in the database.");
                return ApiResponse<IEnumerable<TeamRole>>.Error(HttpStatusCode.NotFound, "لا توجد أدوار فرق مسجلة");
            }

            _logger.LogInformation("{Count} team roles retrieved from the database.", teamRoles.Count);

            return ApiResponse<IEnumerable<TeamRole>>.Success(teamRoles, "تم جلب أدوار الفرق بنجاح");
        }


        public async Task<ApiResponse<TeamRole>> UpdateAsync(int id, string newRoleName)
        {
            var teamRole = await _context.TeamRoles.FindAsync(id);
            if (teamRole == null)
            {
                _logger.LogWarning("Team role with ID {RoleId} not found", id);
                return ApiResponse<TeamRole>.Error(HttpStatusCode.NotFound, "الدور غير موجود");
            }

            var isNameExists = await _context.TeamRoles
                .AnyAsync(x => x.Name == newRoleName && x.Id != id);
            if (isNameExists)
            {
                _logger.LogWarning("Team role name '{RoleName}' is already in use", newRoleName);
                return ApiResponse<TeamRole>.Error(HttpStatusCode.Conflict, "اسم الدور مستخدم من قبل");
            }

            teamRole.Name = newRoleName;

            _context.TeamRoles.Update(teamRole);
            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to update team role with ID {RoleId}", id);
                return ApiResponse<TeamRole>.Error(HttpStatusCode.BadRequest, "فشل في تحديث الدور");
            }

            _logger.LogInformation("Team role with ID {RoleId} updated successfully", id);
            return ApiResponse<TeamRole>.Success(teamRole, "تم تحديث الدور بنجاح");
        }


        public async Task<ApiResponse<TeamRole>> GetByIdAsync(int roleId)
        {
            var teamRole = await _context.TeamRoles.FindAsync(roleId);
            if (teamRole == null)
            {
                _logger.LogWarning("Team role with ID {RoleId} not found", roleId);
                return ApiResponse<TeamRole>.Error(HttpStatusCode.NotFound, "الدور غير موجود");
            }

            _logger.LogInformation("Team role with ID {RoleId} retrieved successfully", roleId);
            return ApiResponse<TeamRole>.Success(teamRole, "تم جلب الدور بنجاح");
        }


        public async Task<ApiResponse<TeamRole>> AddAsync(string RoleName)
        {
            var isTeamRoleExists = _context.TeamRoles.Any(x => x.Name == RoleName);
            if (isTeamRoleExists)
            {
                _logger.LogWarning("Team role with name '{RoleName}' already exists", RoleName);
                return ApiResponse<TeamRole>.Error(HttpStatusCode.Conflict, "اسم الدور مستخدم من قبل");
            }

            var teamRole = new TeamRole
            {
                Name = RoleName,
            };

            await _context.TeamRoles.AddAsync(teamRole);
            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to save team role to the database");
                return ApiResponse<TeamRole>.Error(HttpStatusCode.BadRequest, "فشل في إضافة الدور");
            }

            _logger.LogInformation("Team role with ID {RoleId} added successfully", teamRole.Id);
            return ApiResponse<TeamRole>.Success(teamRole, "تمت الإضافة بنجاح");

        }
        public async Task<ApiResponse<TeamRole>> DeleteAsync(int roleId)
        {
            var teamRole = await _context.TeamRoles.FindAsync(roleId);
            if (teamRole == null)
            {
                _logger.LogWarning("Team role with ID {RoleId} not found", roleId);
                return ApiResponse<TeamRole>.Error(HttpStatusCode.NotFound, "الدور غير موجود");
            }

            _context.TeamRoles.Remove(teamRole);

            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to delete team role with ID {RoleId}", roleId);
                return ApiResponse<TeamRole>.Error(HttpStatusCode.BadRequest, "فشل في حذف الدور");
            }

            _logger.LogInformation("Team role with ID {RoleId} deleted successfully", roleId);
            return ApiResponse<TeamRole>.Success(teamRole, "تم حذف الدور بنجاح");
        }

    }
}
