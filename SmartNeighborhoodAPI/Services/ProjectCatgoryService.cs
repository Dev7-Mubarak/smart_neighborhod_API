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

            var existProjectCatgory= await _context.ProjectCatogories.FirstOrDefaultAsync(x => x.Name ==ProjectCatgoryDto.Name);
            if (existProjectCatgory != null)
                return ApiResponse<ProjectCatgoryDto>.Error(HttpStatusCode.Conflict, "Project Catgory Is Already Exist");




            await _context.ProjectCatogories.AddAsync(ProjectCatogory);
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
            var ProjectCatogorys = await _context.ProjectCatogories.AsNoTracking().ToListAsync();
            if (ProjectCatogorys.Count > 0)
            {
                var ProjectCatgoryDtos = _mapper.Map<IEnumerable<ProjectCatgoryDto>>(ProjectCatogorys);
                return ApiResponse<IEnumerable<ProjectCatgoryDto>>.Success(ProjectCatgoryDtos);
            }

            return ApiResponse<IEnumerable<ProjectCatgoryDto>>.Error(HttpStatusCode.NotFound, "No ProjectCatogorys Found");



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
