using OurProjectSmartNeiborhood.Services;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[EnableRateLimiting("fixed-window")]
    public class PersonController : AppControllerBase
    {
        private readonly PersonService _PersonService;

        public PersonController(PersonService PersonService)
        {
            _PersonService = PersonService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)
        {
            var result = await _PersonService.GetAllAsync(pageNumber, pageSize, search);
            return Response(result);
        }

        [HttpPost("[action]")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddAsync([FromForm] CreatePersonDto createPersonDto)
        {
            var result = await _PersonService.AddAsync(createPersonDto);
            return Response(result);
        }

        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _PersonService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm] CreatePersonDto dto)
        {
            var result = await _PersonService.UpdateAsync(id, dto);
            return Response(result);
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _PersonService.DeleteAsync(id);
            return Response(result);
        }
    }
}

