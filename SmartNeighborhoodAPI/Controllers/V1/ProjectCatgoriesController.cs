namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[EnableRateLimiting("fixed-window")]
    public class ProjectCatgoriesController : AppControllerBase
    {
        private readonly ProjectCatgoryService _ProjectCatgoryService;
        
        




        public ProjectCatgoriesController(ProjectCatgoryService ProjectCatgoryService)
        {
            _ProjectCatgoryService = ProjectCatgoryService;


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(ProjectCatgoryDto ProjectCatgoryDto)
        {
            var result = await _ProjectCatgoryService.AddAsync(ProjectCatgoryDto);
            return Response(result);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _ProjectCatgoryService.GetAll();

            return Response(result);
        }
        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _ProjectCatgoryService.GetByIdAsync(id);

            return Response(result);



        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, ProjectCatgoryDto ProjectCatgoryDto)
        {
            var result = await _ProjectCatgoryService.UpdateAsync(id, ProjectCatgoryDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _ProjectCatgoryService.DeleteAsync(id);

            return Response(result);
        }
    }
}
