using SmartNeighborhoodAPI.Helpers.DTOs.ConflictType;

namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfilctCaseTypeController : AppControllerBase
    {
        private readonly ConflictTypeService _conflictTypeService;

        public ConfilctCaseTypeController(ConflictTypeService conflictTypeService)
        {
            _conflictTypeService = conflictTypeService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(AddConflictTypeDto addConflictTypeDto)
        {
            var result = await _conflictTypeService.AddAsync(addConflictTypeDto);
            return Response(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _conflictTypeService.GetAll();
            return Response(result);
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _conflictTypeService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateConflictTypeDto updateConflictTypeDto)
        {
            var result = await _conflictTypeService.UpdateAsync(id, updateConflictTypeDto);
            return Response(result);
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _conflictTypeService.DeleteAsync(id);
            return Response(result);
        }
    }

}
