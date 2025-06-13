using System.Net;
using Microsoft.Extensions.Logging;
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
        public async Task<ApiResponse<ProjectDto>> AddAsync(ProjectDto projectDto)
        {
            var managerExists = await _context.People.AnyAsync(u => u.Id == projectDto.ManagerId);
            if (!managerExists)
            {
                _logger.LogWarning("AddAsync failed: Manager with ID {ManagerId} does not exist.", projectDto.ManagerId);
                return ApiResponse<ProjectDto>.Error(HttpStatusCode.NotFound, $"المدير برقم {projectDto.ManagerId} غير موجود");
            }
            var categoryExists = await _context.ProjectCatogories.AnyAsync(u => u.Id == projectDto.ProjectCatgoryId);
            if (!categoryExists)
            {
                _logger.LogWarning("AddAsync failed: Project Category with ID {ProjectCatgoryId} does not exist.", projectDto.ProjectCatgoryId);
                return ApiResponse<ProjectDto>.Error(HttpStatusCode.NotFound, $" {projectDto.ProjectCatgoryId} غير موجود");
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
                _logger.LogInformation("Project added successfully: {@Project}", project);
                return ApiResponse<ProjectDto>.Success(projectDto, "تمت إضافة المشروع بنجاح");
            }

            _logger.LogWarning("AddAsync failed: SaveChanges returned 0.");
            return ApiResponse<ProjectDto>.Error(HttpStatusCode.BadRequest, "لم يتم حفظ المشروع");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id); ;
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Project Not Found");

            _context.Projects.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Project Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the Project");
        }
        public async Task<ApiResponse<IEnumerable<ReturnProjectDto>>> GetAll()
        {
            _logger.LogInformation("Fetching all Projects");

            var projects = await _context.Projects
                .Include(x => x.ProjectCatogory)
                .Include(x => x.Manager)
                .AsNoTracking()
                .ToListAsync();

            if (projects.Count > 0)
            {
                var projectDtos = projects.Select(project => new ReturnProjectDto
                {
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

            _logger.LogWarning("No projects found in the database.");

            return ApiResponse<IEnumerable<ReturnProjectDto>>.Error(HttpStatusCode.NotFound, "لا توجد مشاريع");
        }

        public async Task<ApiResponse<ProjectDto>> GetByIdAsync(int id)
        {
            var Project = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (Project == null)
                return ApiResponse<ProjectDto>.Error(HttpStatusCode.NotFound, "Project Not Found");


            var ProjectDto = _mapper.Map<ProjectDto>(Project);
            return ApiResponse<ProjectDto>.Success(ProjectDto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, ProjectDto ProjectDto)
        {
            var ExsitProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitProject is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Project Not Found");
            var UpdateProject = _mapper.Map(ProjectDto, ExsitProject);

            _context.Projects.Update(UpdateProject);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Project Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update Project");


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
