namespace SmartNeighborhoodAPI.Controllers
{
    public class BlocksController : AppControllerBase
    {
        private readonly BlockServices _BlockServices;

        public BlocksController(BlockServices BlockServices)
        {
            _BlockServices = BlockServices;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(BlockDto BlockDto)
        {
            return Response(await _BlockServices.AddAsync(BlockDto));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _BlockServices.GetAll());

        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Response(await _BlockServices.GetByIdAsync(id));
        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, BlockDto BlockDto)
        {
            return Response(await _BlockServices.UpdateAsync(id, BlockDto));
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Response(await _BlockServices.DeleteAsync(id));
        }
    }

}
