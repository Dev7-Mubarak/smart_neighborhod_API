namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplainTypesController : AppControllerBase
    {
        private readonly ComplainTypeService _ComplainTypeService;




        public ComplainTypesController(ComplainTypeService ComplainTypeService)
        {
            _ComplainTypeService = ComplainTypeService;


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(ComplainTypeDto ComplainTypeDto)
        {
            var result = await _ComplainTypeService.AddAsync(ComplainTypeDto);

            return Response(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _ComplainTypeService.GetAll();

            return Response(result);
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _ComplainTypeService.GetByIdAsync(id);

            return Response(result);



        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, ComplainTypeDto ComplainTypeDto)
        {
            var result = await _ComplainTypeService.UpdateAsync(id, ComplainTypeDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _ComplainTypeService.DeleteAsync(id);

            return Response(result);
        }
    }
}
