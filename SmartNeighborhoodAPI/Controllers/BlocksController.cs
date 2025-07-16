using Microsoft.AspNetCore.RateLimiting;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;

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
        public async Task<IActionResult> GetDetails(
            int blockId,
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)

        {
            return Response(await _BlockServices.GetDetails(blockId, pageNumber, pageSize, search));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(BlockDto BlockDto)
        {
            return Response(await _BlockServices.AddAsync(BlockDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeBlockManager(ChangeBlockManagerDto blockManagerDto)
        {
            return Response(await _BlockServices.ChangeBlockManager(blockManagerDto));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _BlockServices.GetAllAsync());
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
