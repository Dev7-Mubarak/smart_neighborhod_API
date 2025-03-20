namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonComplainsController : AppControllerBase
    {
        private readonly PersonComplainService _PersonComplainService;
        




        public PersonComplainsController(PersonComplainService PersonComplainService)
        {
            _PersonComplainService = PersonComplainService;


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(PersonComplainDto PersonComplainDto)
        {
            var result = await _PersonComplainService.AddAsync(PersonComplainDto);
            return Response(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _PersonComplainService.GetAll();

            return Response(result);
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _PersonComplainService.GetByIdAsync(id);

            return Response(result);



        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, PersonComplainDto PersonComplainDto)
        {
            var result = await _PersonComplainService.UpdateAsync(id, PersonComplainDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _PersonComplainService.DeleteAsync(id);

            return Response(result);
        }
    }
}
