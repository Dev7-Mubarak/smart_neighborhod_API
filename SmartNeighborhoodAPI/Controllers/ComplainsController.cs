//using SmartNeighborhoodAPI.Helpers.DTOs;

//namespace SmartNeighborhoodAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ComplainsController : AppControllerBase
//    {
//        private readonly ComplainService _ComplainService;





//        public ComplainsController(ComplainService ComplainService)
//        {
//            _ComplainService = ComplainService;


//        }
//        [HttpPost("[action]")]
//        public async Task<IActionResult> AddAsync(ComplainDTo ComplainDto)
//        {
//            var result = await _ComplainService.AddAsync(ComplainDto);
//            return Response(result);
//        }
//        [HttpGet("[action]")]
//        public async Task<IActionResult> GetAllAsync()
//        {
//            var result = await _ComplainService.GetAll();

//            return Response(result);
//        }
//        [HttpGet("[action]/{id:int}")]
//        public async Task<IActionResult> GetByIdAsync(int id)
//        {
//            var result = await _ComplainService.GetByIdAsync(id);

//            return Response(result);



//        }
//        [HttpPut("[action]/{id:int}")]
//        public async Task<IActionResult> UpdateAsync(int id, ComplainDTo ComplainDto)
//        {
//            var result = await _ComplainService.UpdateAsync(id, ComplainDto);

//            return Response(result);
//        }
//        [HttpDelete("[action]/{id:int}")]
//        public async Task<IActionResult> DeleteAsync(int id)
//        {
//            var result = await _ComplainService.DeleteAsync(id);

//            return Response(result);
//        }
//    }
//}

