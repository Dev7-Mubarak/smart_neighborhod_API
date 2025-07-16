using SmartNeighborhoodAPI.Helpers.DTOs;
using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;

namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConflictCaseController : AppControllerBase
    {
        private readonly ConflictCaseService _conflictCaseService;

        public ConflictCaseController(ConflictCaseService conflictCaseService)
        {
            _conflictCaseService = conflictCaseService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(AddConflictCaseDto conflictCaseDto)
        {
            var result = await _conflictCaseService.AddAsync(conflictCaseDto);
            return Response(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _conflictCaseService.GetAll();
            return Response(result);
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _conflictCaseService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateConflictCaseDto conflictCaseDto)
        {
            var result = await _conflictCaseService.UpdateAsync(id, conflictCaseDto);
            return Response(result);
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _conflictCaseService.DeleteAsync(id);
            return Response(result);
        }
    }

}

