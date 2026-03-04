using Microsoft.AspNetCore.Authorization;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Authorize]
    [SwaggerTag("Blocks management endpoints")]
    public class BlocksController : AppControllerBase
    {
        private readonly IBlockServices _blockServices;

        public BlocksController(IBlockServices blockServices)
        {
            _blockServices = blockServices;
        }

        [HttpPost(Router.Blocks.Add)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Creates a new block",
            Description = "Creates a new block and returns the created result."
        )]
        [ProducesResponseType(typeof(RetrunBlockDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync(
            [FromBody, SwaggerParameter("Block data to create", Required = true)] BlockDto blockDto)
        {
            return Response(await _blockServices.AddAsync(blockDto));
        }

        [HttpPut(Router.Blocks.ChangeManager)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Change block manager",
            Description = "Changes the manager of a block and optionally creates a new manager account if needed."
        )]
        [ProducesResponseType(typeof(RetrunBlockDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ChangeBlockManager(
            [FromRoute, SwaggerParameter("Block ID", Required = true)] int id,
            [FromBody, SwaggerParameter("Manager change details", Required = true)] ChangeManagerDto blockManagerDto)
        {
            return Response(await _blockServices.ChangeManager(id, blockManagerDto));
        }

        [HttpGet(Router.Blocks.GetAll)]
        [SwaggerOperation(Summary = "Retrieve all blocks", Description = "Retrieves all blocks accessible by the user.")]
        [ProducesResponseType(typeof(IEnumerable<RetrunBlockDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _blockServices.GetAllAsync());
        }

        [HttpGet(Router.Blocks.GetById)]
        [SwaggerOperation(Summary = "Get block by ID", Description = "Retrieve a block by its ID.")]
        // NOTE: IBlockServices.GetByIdAsync returns ApiResponse<Block> (domain entity).
        // The interface contract should be updated to return ApiResponse<RetrunBlockDto> to
        // prevent domain model exposure. The ProducesResponseType below reflects the correct target.
        [ProducesResponseType(typeof(RetrunBlockDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute, SwaggerParameter("Block ID to retrieve", Required = true)] int id)
        {
            return Response(await _blockServices.GetByIdAsync(id));
        }

        [HttpPut(Router.Blocks.Update)]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update block", Description = "Updates the data of an existing block.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Block ID to update", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated block data", Required = true)] UpdateBlockDto blockDto)
        {
            return Response(await _blockServices.UpdateAsync(id, blockDto));
        }

        [HttpDelete(Router.Blocks.Delete)]
        [SwaggerOperation(Summary = "Delete block", Description = "Deletes a specific block by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute, SwaggerParameter("Block ID to delete", Required = true)] int id)
        {
            return Response(await _blockServices.DeleteAsync(id));
        }

        [HttpGet(Router.Blocks.Dashboard)]
        [SwaggerOperation(Summary = "Get blocks dashboard (Admin only)", Description = "Returns dashboard statistics for all blocks.")]
        [ProducesResponseType(typeof(BlockDashboardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardAsync(CancellationToken ct)
        {
            return Response(await _blockServices.GetDashboardAsync(ct));
        }

        [HttpGet(Router.Blocks.GetFamilies)]
        [SwaggerOperation(
            Summary = "Get families for a block",
            Description = "Returns all families within a specific block.")]
        [ProducesResponseType(typeof(ApiResponse<ReturnBlockFamiliesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFamiliesAsync(
            [FromRoute, SwaggerParameter("Block ID to retrieve families for", Required = true)] int id)
        {
            return Response(await _blockServices.GetFamiliesAsync(id));
        }

        [HttpGet(Router.Blocks.GetMyDashboard)]
        [SwaggerOperation(
            Summary = "Get my dashboard statistics (Block Manager only)",
            Description = "Returns dashboard statistics for the authenticated block manager.")]
        [ProducesResponseType(typeof(BlockDashboardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetMyDashboard(CancellationToken ct)
        {
            return Response(await _blockServices.GetMyDashboardAsync(ct));
        }

        // NOTE: IBlockServices.GetMyBlocksAsync(string userId, ...) forces identity resolution
        // into the controller, violating SoC. The interface should be refactored to
        // GetMyBlocksAsync(CancellationToken) so the service resolves the caller via
        // UserContextService, matching the pattern used in ConflictCaseService.
        [HttpGet(Router.Blocks.GetMyBlocks)]
        [SwaggerOperation(
            Summary = "Get my managed blocks (Block Manager only)",
            Description = "Returns all blocks managed by the authenticated user.")]
        [ProducesResponseType(typeof(ApiResponse<List<RetrunBlockDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetMyBlocks(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Response(await _blockServices.GetMyBlocksAsync(userId, ct));
        }
    }
}
