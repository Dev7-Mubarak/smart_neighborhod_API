namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[EnableRateLimiting("fixed-window")]
    public class FamilyTypesController : AppControllerBase
    {
        private readonly FamilyTypeService _FamilyTypeService;


        public FamilyTypesController(FamilyTypeService FamilyTypeService)
        {
            _FamilyTypeService = FamilyTypeService;

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(string nameFamilyTypeDto)
        {
            var result = await _FamilyTypeService.AddAsync(nameFamilyTypeDto);
            return Response(result);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _FamilyTypeService.GetAll();
            return Response(result);

        }
        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _FamilyTypeService.GetByIdAsync(id);
            return Response(result);
        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, string nameFamilyTypeDto)
        {
            var result = await _FamilyTypeService.UpdateAsync(id, nameFamilyTypeDto);
            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _FamilyTypeService.DeleteAsync(id);
            return Response(result);
        }
    }
}



