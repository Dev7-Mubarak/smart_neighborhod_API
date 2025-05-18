using OurProjectSmartNeiborhood.Services;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : AppControllerBase
    {
        private readonly PersonService _PersonService;

        public PersonController(PersonService PersonService)
        {
            _PersonService = PersonService;
        }

        [HttpPost("[action]")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddAsync([FromForm] CreatePersonDto createPersonDto)
        {
            var result = await _PersonService.AddAsync(createPersonDto);
            return Response(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _PersonService.GetAll();
            return Response(result);
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _PersonService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, PersonDto personDto)
        {
            var result = await _PersonService.UpdateAsync(id, personDto);
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

