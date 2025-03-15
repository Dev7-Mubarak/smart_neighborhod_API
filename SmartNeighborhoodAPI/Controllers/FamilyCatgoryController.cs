using Microsoft.AspNetCore.Authorization;

namespace SmartNeighborhoodAPI.Controllers
{
    [Authorize]
    public class FamilyCatgoryController : AppControllerBase
    {
        private readonly FamilyCatgoryService _FamilyCatgoryService;

        public FamilyCatgoryController(FamilyCatgoryService FamilyCatgoryService)
        {
            _FamilyCatgoryService = FamilyCatgoryService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(string nameFamilyCatgory)
        {
            return Response(await _FamilyCatgoryService.AddAsync(nameFamilyCatgory));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _FamilyCatgoryService.GetAll());
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Response(await _FamilyCatgoryService.GetByIdAsync(id));
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, string nameFamilyCatgory)
        {
            return Response(await _FamilyCatgoryService.UpdateAsync(id, nameFamilyCatgory));
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Response(await _FamilyCatgoryService.DeleteAsync(id));
        }
    }
}
