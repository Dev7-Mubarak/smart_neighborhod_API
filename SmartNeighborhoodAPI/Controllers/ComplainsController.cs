using SmartNeighborhoodAPI.Helpers.DTOs;

namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplainsController : AppControllerBase
    {
        private readonly ComplainService _ComplainService;





        public ComplainsController(ComplainService ComplainService)
        {
            _ComplainService = ComplainService;


        }
   
        [HttpGet("GetAllManagers")]
        public async Task<IActionResult> GetAllManagers()
        {
            var result = await _ComplainService.GetAllManagersAsync();
            return Response(result);
        }
    
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _ComplainService.GetByIdAsync(id);

            return Response(result);



        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, AddComplainDto dto)
        {
            var result = await _ComplainService.UpdateAsync(id, dto);
            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _ComplainService.DeleteAsync(id);

            return Response(result);
        }
    }
}

