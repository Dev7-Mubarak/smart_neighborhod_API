namespace SmartNeighborhoodAPI.Controllers
{
    public class BlocksController : AppControllerBase
    {
        private readonly BlockServices _BlockServices;

        public BlocksController(BlockServices BlockServices)
        {
            _BlockServices = BlockServices;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBlockFamiliesById(
            int blockId,
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)

        {
            return Response(await _BlockServices.GetBlockFamiliesById(blockId, pageNumber, pageSize, search));
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
