using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ProjectService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;

        public ProjectService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }
        public async Task<ApiResponse<ProjectDto>> AddAsync(ProjectDto ProjectDto)
        {

            var Project = _mapper.Map<Project>(ProjectDto);



            await _context.Projects.AddAsync(Project);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<ProjectDto>.Success(ProjectDto, "Added Successed");

            return ApiResponse<ProjectDto>.Error(HttpStatusCode.BadRequest, "Project not add");


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
        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetAll()
        {
            var Projects = await _context.Projects.AsNoTracking().ToListAsync();
            if (Projects.Count > 0)
            {
                var ProjectDtos = _mapper.Map<IEnumerable<ProjectDto>>(Projects);
                return ApiResponse<IEnumerable<ProjectDto>>.Success(ProjectDtos);
            }

            return ApiResponse<IEnumerable<ProjectDto>>.Error(HttpStatusCode.NotFound, "No Projects Found");



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
    }
}
