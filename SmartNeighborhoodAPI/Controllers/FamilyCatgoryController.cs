namespace SmartNeighborhoodAPI.Controllers
{
    public class FamilyCatgoryController : AppControllerBase
    {
        private readonly FamilyCatgoryService _familyCatgoryService;

        public FamilyCatgoryController(FamilyCatgoryService familyCatgoryService)
        {
            _familyCatgoryService = familyCatgoryService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(string name)
        {
            var result = await _familyCatgoryService.AddAsync(name);
            return Response(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _familyCatgoryService.GetAll();
            return Response(result);

        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _familyCatgoryService.GetByIdAsync(id);
            return Response(result);
        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, string nameFamilyTypeDto)
        {
            var result = await _familyCatgoryService.UpdateAsync(id, nameFamilyTypeDto);
            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _familyCatgoryService.DeleteAsync(id);
            return Response(result);
        }
    }
}



