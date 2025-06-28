using System.Net;
using Microsoft.Extensions.Logging;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
namespace SmartNeighborhoodAPI.Services
{
    public class ProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<Project> _logger;

        public ProjectService(ApplicationDbContext context, IMapper mapper, ILogger<Project> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<ReturnProjectDto>> AddAsync(ProjectDto projectDto)
        {
            var managerExists = await _context.People.AnyAsync(u => u.Id == projectDto.ManagerId);
            if (!managerExists)
            {
                _logger.LogWarning("AddAsync failed: Manager with ID {ManagerId} does not exist.", projectDto.ManagerId);
                return ApiResponse<ReturnProjectDto>.Error(HttpStatusCode.NotFound, $"المدير برقم {projectDto.ManagerId} غير موجود");
            }
            var categoryExists = await _context.ProjectCatogories.AnyAsync(u => u.Id == projectDto.ProjectCatgoryId);
            if (!categoryExists)
            {
                _logger.LogWarning("AddAsync failed: Project Category with ID {ProjectCatgoryId} does not exist.", projectDto.ProjectCatgoryId);
                return ApiResponse<ReturnProjectDto>.Error(HttpStatusCode.NotFound, $" {projectDto.ProjectCatgoryId} غير موجود");
            }

            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                ManagerId = projectDto.ManagerId,
                ProjectCatogoryId = projectDto.ProjectCatgoryId,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                ProjectStatus = projectDto.ProjectStatus,
                ProjectPriority = projectDto.ProjectPriority,
                Budget = projectDto.Budget
            };

            await _context.Projects.AddAsync(project);
            if (await _context.SaveChangesAsync() > 0)
            {

                var projectResult = await GetByIdAsync(project.Id);
                if (!projectResult.IsSuccess)
                    return projectResult;

                _logger.LogInformation("Project added successfully: {@Project}", projectResult.Data);

                return ApiResponse<ReturnProjectDto>.Success(projectResult.Data, "تمت إضافة المشروع بنجاح");
            }

            _logger.LogWarning("AddAsync failed: SaveChanges returned 0.");
            return ApiResponse<ReturnProjectDto>.Error(HttpStatusCode.BadRequest, "لم يتم حفظ المشروع");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Delete request received for Project with ID: {ProjectId}", id);

            var entity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("Delete failed: Project with ID {ProjectId} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المشروع غير موجود");
            }

            _context.Projects.Remove(entity);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                _logger.LogInformation("Project with ID {ProjectId} deleted successfully.", id);
                return ApiResponse<string>.Success("تم حذف المشروع بنجاح");
            }

            _logger.LogError("Delete failed: SaveChanges returned 0 for Project ID {ProjectId}.", id);
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل حذف المشروع");
        }

        public async Task<ApiResponse<IEnumerable<ReturnProjectDto>>> GetAll(int? ProjectCategoryId)
        {
            _logger.LogInformation("Fetching all Projects{CategoryFilter}",
                ProjectCategoryId.HasValue ? $" with CategoryId = {ProjectCategoryId}" : "");

            var query = _context.Projects
                .Include(x => x.ProjectCatogory)
                .Include(x => x.Manager)
                .AsNoTracking()
                .AsQueryable();

            if (ProjectCategoryId.HasValue)
            {
                query = query.Where(p => p.ProjectCatogoryId == ProjectCategoryId.Value);
            }

            var projects = await query.ToListAsync();

            if (projects.Count > 0)
            {
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
                    Manager = new CustomPersonDto { Id = project.Manager.Id, FullName = project.Manager.FullName },
                    ProjectCatgory = project.ProjectCatogory
                }).ToList();

                _logger.LogInformation("Retrieved {Count} projects successfully.", projectDtos.Count);

                return ApiResponse<IEnumerable<ReturnProjectDto>>.Success(projectDtos);
            }

            _logger.LogWarning("No projects found{Filter}.",
                ProjectCategoryId.HasValue ? $" for CategoryId {ProjectCategoryId}" : "");

            return ApiResponse<IEnumerable<ReturnProjectDto>>.Error(HttpStatusCode.NotFound, "لا توجد مشاريع");
        }

        public async Task<ApiResponse<ReturnProjectDto>> GetByIdAsync(int id)
        {
            var project = await _context.Projects
                .Include(x => x.Manager)
                .Include(x => x.ProjectCatogory)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
            {
                _logger.LogError("Project not found with ID {ProjectId}.", id);
                return ApiResponse<ReturnProjectDto>.Error(HttpStatusCode.NotFound, "المشروع غير موجود");
            }

            var returnDto = new ReturnProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Budget = project.Budget,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                ProjectStatus = GetDisplayName(project.ProjectStatus),
                ProjectPriority = GetDisplayName(project.ProjectPriority),
                Manager = new CustomPersonDto
                {
                    Id = project.Manager.Id,
                    FullName = project.Manager.FullName
                },
                ProjectCatgory = project.ProjectCatogory
            };

            return ApiResponse<ReturnProjectDto>.Success(returnDto);
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, ProjectDto projectDto)
        {
            _logger.LogInformation("Starting update process for project with ID {ProjectId}.", id);

            // Check if project exists
            var existingProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProject is null)
            {
                _logger.LogWarning("Project with ID {ProjectId} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المشروع غير موجود");
            }

            // Check if Manager exists
            var managerExists = await _context.People.AnyAsync(p => p.Id == projectDto.ManagerId);
            if (!managerExists)
            {
                _logger.LogWarning("Manager with ID {ManagerId} not found.", projectDto.ManagerId);
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "المدير غير موجود");
            }

            // Check if Project Category exists
            var categoryExists = await _context.ProjectCatogories.AnyAsync(c => c.Id == projectDto.ProjectCatgoryId);
            if (!categoryExists)
            {
                _logger.LogWarning("Project category with ID {CategoryId} not found.", projectDto.ProjectCatgoryId);
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "فئة المشروع غير موجودة");
            }

            // Manual property update
            existingProject.Name = projectDto.Name;
            existingProject.Description = projectDto.Description;
            existingProject.ManagerId = projectDto.ManagerId;
            existingProject.ProjectCatogoryId = projectDto.ProjectCatgoryId;
            existingProject.StartDate = projectDto.StartDate;
            existingProject.EndDate = projectDto.EndDate;
            existingProject.ProjectStatus = projectDto.ProjectStatus;
            existingProject.Budget = projectDto.Budget;
            existingProject.ProjectPriority = projectDto.ProjectPriority;

            _context.Projects.Update(existingProject);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("Project with ID {ProjectId} updated successfully.", id);
                return ApiResponse<string>.Success("تم تحديث المشروع بنجاح");
            }

            _logger.LogError("Failed to update project with ID {ProjectId}.", id);
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في تحديث المشروع");
        }

        public async Task<ApiResponse<string>> AssignTeamToProjectAsync(int projectId, int teamId)
        {
            _logger.LogInformation("Starting assignment of Team {TeamId} to Project {ProjectId}", teamId, projectId);

            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                _logger.LogWarning("Project with ID {ProjectId} not found.", projectId);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المشروع غير موجود");
            }

            var team = await _context.Teams.FindAsync(teamId);
            if (team == null)
            {
                _logger.LogWarning("Team with ID {TeamId} not found.", teamId);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الفريق غير موجود");
            }

            var exists = await _context.ProjectTeams.AnyAsync(pt => pt.ProjectId == projectId && pt.TeamId == teamId);
            if (exists)
            {
                _logger.LogWarning("Team {TeamId} is already assigned to Project {ProjectId}.", teamId, projectId);
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "الفريق مرتبط بالفعل بالمشروع");
            }

            var projectTeam = new ProjectTeam
            {
                ProjectId = projectId,
                TeamId = teamId
            };

            await _context.ProjectTeams.AddAsync(projectTeam);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult > 0)
            {
                _logger.LogInformation("Successfully assigned Team {TeamId} to Project {ProjectId}.", teamId, projectId);
                return ApiResponse<string>.Success("تم ربط الفريق بالمشروع بنجاح");
            }

            _logger.LogError("Failed to assign Team {TeamId} to Project {ProjectId}.", teamId, projectId);
            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "فشل في ربط الفريق بالمشروع");
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
