using SmartNeighborhoodAPI.Services;

namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectFamilyController : AppControllerBase
    {
        private readonly ProjectFamilieservice _ProjectFamilyService;
        
        



        public ProjectFamilyController(ProjectFamilieservice ProjectFamilyService)
        {
            _ProjectFamilyService = ProjectFamilyService;


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(ProjectFamilyDto ProjectFamilyDto)
        {
            var result = await _ProjectFamilyService.AddAsync(ProjectFamilyDto);
            return Response(result);
            
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _ProjectFamilyService.GetAll();

            return Response(result);
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _ProjectFamilyService.GetByIdAsync(id);

            return Response(result);



        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, ProjectFamilyDto ProjectFamilyDto)
        {
            var result = await _ProjectFamilyService.UpdateAsync(id, ProjectFamilyDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _ProjectFamilyService.DeleteAsync(id);

            return Response(result);
        }
    }
}
