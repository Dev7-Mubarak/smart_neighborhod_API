using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ProjectFamilieservice : IProjectFamilieservice
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;


        public ProjectFamilieservice(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<ProjectFamilyDto>> AddAsync(ProjectFamilyDto ProjectFamilyDto)
        {

            var ProjectFamily = _mapper.Map<ProjectFamily>(ProjectFamilyDto);


            await _context.AddAsync(ProjectFamily);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<ProjectFamilyDto>.Success(ProjectFamilyDto, "Added Successed");

            return ApiResponse<ProjectFamilyDto>.Error(HttpStatusCode.BadRequest, "ProjectFamily not add");


        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.ProjectFamilies.FirstOrDefaultAsync(x => x.Id == id); ;
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ProjectFamily Not Found");

            _context.ProjectFamilies.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("ProjectFamily Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the ProjectFamily");
        }
        public async Task<ApiResponse<IEnumerable<ProjectFamilyDto>>> GetAll()
        {
            var ProjectFamilies = await _context.ProjectFamilies.AsNoTracking().ToListAsync();
            if (ProjectFamilies.Count > 0)
            {
                var ProjectFamilyDtos = _mapper.Map<IEnumerable<ProjectFamilyDto>>(ProjectFamilies);
                return ApiResponse<IEnumerable<ProjectFamilyDto>>.Success(ProjectFamilyDtos);
            }

            return ApiResponse<IEnumerable<ProjectFamilyDto>>.Error(HttpStatusCode.NotFound, "No ProjectFamilies Found");



        }
        public async Task<ApiResponse<ProjectFamilyDto>> GetByIdAsync(int id)
        {
            var ProjectFamily = await _context.ProjectFamilies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (ProjectFamily == null)
                return ApiResponse<ProjectFamilyDto>.Error(HttpStatusCode.NotFound, "ProjectFamily Not Found");


            var ProjectFamilyDto = _mapper.Map<ProjectFamilyDto>(ProjectFamily);
            return ApiResponse<ProjectFamilyDto>.Success(ProjectFamilyDto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, ProjectFamilyDto ProjectFamilyDto)
        {
            var ExsitProjectFamily = await _context.ProjectFamilies.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitProjectFamily is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ProjectFamily Not Found");
            var UpdateProjectFamily = _mapper.Map(ProjectFamilyDto, ExsitProjectFamily);

            _context.ProjectFamilies.Update(UpdateProjectFamily);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("ProjectFamily Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update ProjectFamily");


        }
    }
}
