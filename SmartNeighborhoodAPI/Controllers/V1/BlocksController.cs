using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ValidateActionFilter]
    [ApiVersion("1.0")]
    //[EnableRateLimiting("fixed-window")]
    [Route("api/[controller]")]
    public class BlocksController : AppControllerBase
    {
        private readonly BlockServices _BlockServices;

        public BlocksController(BlockServices BlockServices)
        {
            _BlockServices = BlockServices;
        }

        [HttpGet("get-details")]
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
        [HttpPost("change-block-manager")]
        public async Task<IActionResult> ChangeBlockManager(ChangeBlockManagerDto blockManagerDto)
        {
            return Response(await _BlockServices.ChangeBlockManager(blockManagerDto));
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _BlockServices.GetAllAsync());
        }
        [HttpGet("get-by-id/{id:int}")]
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
