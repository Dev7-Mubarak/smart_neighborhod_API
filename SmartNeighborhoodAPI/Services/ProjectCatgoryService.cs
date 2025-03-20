using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ProjectCatgoryService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;

        public ProjectCatgoryService(ApplicationDbContext context, IMapper mapper)
        {
            
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<ProjectCatgoryDto>> AddAsync(ProjectCatgoryDto ProjectCatgoryDto)
        {
            

            var ProjectCatogory = _mapper.Map<ProjectCatogory>(ProjectCatgoryDto);

            var existProjectCatgory= await _context.FamilyTypes.FirstOrDefaultAsync(x => x.Name == ProjectCatgoryDto.Name);
            if (existProjectCatgory != null)
                return ApiResponse<ProjectCatgoryDto>.Error(HttpStatusCode.Conflict, "FamilyType Is Already Exist");




            await _context.AddAsync(ProjectCatogory);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<ProjectCatgoryDto>.Success(ProjectCatgoryDto, "Added Successed");

            return ApiResponse<ProjectCatgoryDto>.Error(HttpStatusCode.BadRequest, "ProjectCatogory not add");


        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.ProjectCatogories.FirstOrDefaultAsync(x => x.Id == id); ;
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ProjectCatogory Not Found");

            _context.ProjectCatogories.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("ProjectCatogory Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the ProjectCatogory");
        }
        public async Task<ApiResponse<IEnumerable<ProjectCatgoryDto>>> GetAll()
        {
            var ProjectCatogories = await _context.ProjectCatogories.AsNoTracking().ToListAsync();
            if (ProjectCatogories.Count > 0)
            {
                var ProjectCatgoryDtos = _mapper.Map<IEnumerable<ProjectCatgoryDto>>(ProjectCatogories);
                return ApiResponse<IEnumerable<ProjectCatgoryDto>>.Success(ProjectCatgoryDtos);
            }

            return ApiResponse<IEnumerable<ProjectCatgoryDto>>.Error(HttpStatusCode.NotFound, "No ProjectCatogories Found");



        }
        public async Task<ApiResponse<ProjectCatgoryDto>> GetByIdAsync(int id)
        {
            var ProjectCatogory = await _context.ProjectCatogories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (ProjectCatogory == null)
                return ApiResponse<ProjectCatgoryDto>.Error(HttpStatusCode.NotFound, "ProjectCatogory Not Found");


            var ProjectCatgoryDto = _mapper.Map<ProjectCatgoryDto>(ProjectCatogory);
            return ApiResponse<ProjectCatgoryDto>.Success(ProjectCatgoryDto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, ProjectCatgoryDto ProjectCatgoryDto)
        {
            var ExsitProjectCatogory = await _context.ProjectCatogories.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitProjectCatogory is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ProjectCatogory Not Found");
            var UpdateProjectCatogory = _mapper.Map(ProjectCatgoryDto, ExsitProjectCatogory);

            _context.ProjectCatogories.Update(UpdateProjectCatogory);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("ProjectCatogory Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update ProjectCatogory");


        }
    }
}
