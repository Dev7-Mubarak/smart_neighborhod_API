namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : AppControllerBase
    {
        private readonly FamilyService _FamilyService;

        public FamilyController(FamilyService FamilyService)
        {
            _FamilyService = FamilyService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(FamilyDto FamilyDto)
        {
            var result = await _FamilyService.AddAsync(FamilyDto);
            return Response(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _FamilyService.GetAll();
            return Response(result);
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _FamilyService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, FamilyDto FamilyDto)
        {
            var result = await _FamilyService.UpdateAsync(id, FamilyDto);
            return Response(result);
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _FamilyService.DeleteAsync(id);
            return Response(result);
        }
    }
}
