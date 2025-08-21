using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ValidateActionFilter]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Blocks management endpoints")]
    public class BlocksController : AppControllerBase
    {
        private readonly BlockServices _BlockServices;

        public BlocksController(BlockServices BlockServices)
        {
            _BlockServices = BlockServices;
        }

        [HttpGet("get-details")]
        [SwaggerOperation(Summary = "Get block details", Description = "Returns details for a specific block including families.")]
        [ProducesResponseType(typeof(BlockDetailesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetails(
            [FromQuery, SwaggerParameter("ID of the block", Required = true)] int blockId,
            [FromQuery, SwaggerParameter("Page number", Required = false)] int pageNumber = 1,
            [FromQuery, SwaggerParameter("Page size", Required = false)] int pageSize = 10,
            [FromQuery, SwaggerParameter("Optional search term", Required = false)] string? search = null)
        {
            return Response(await _BlockServices.GetDetails(blockId, pageNumber, pageSize, search));
        }

        [HttpPost("[action]")]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(RetrunBlockDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creates a new block",
            Description = "Creates a new block for the current user and returns the created result."
        )]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Block data to create", Required = true)] BlockDto BlockDto)
        {
            return Response(await _BlockServices.AddAsync(BlockDto));
        }

        [HttpPost("change-block-manager")]
        [SwaggerOperation(
            Summary = "Change block manager",
            Description = "Changes the manager of a block and optionally creates a new manager account if needed."
        )]
        [ProducesResponseType(typeof(RetrunBlockDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ChangeBlockManager([FromBody, SwaggerParameter("Manager change details", Required = true)] ChangeBlockManagerDto blockManagerDto)
        {
            return Response(await _BlockServices.ChangeBlockManager(blockManagerDto));
        }

        [HttpGet("get-all")]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all blocks", Description = "Retrieves all blocks accessible by the user.")]
        [ProducesResponseType(typeof(IEnumerable<RetrunBlockDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _BlockServices.GetAllAsync());
        }

        [HttpGet("get-by-id/{id:int}")]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get block by ID", Description = "Retrieve a block by its ID.")]
        [ProducesResponseType(typeof(Block), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Block ID to retrieve", Required = true)] int id)
        {
            return Response(await _BlockServices.GetByIdAsync(id));
        }

        [HttpPut("[action]/{id:int}")]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update block", Description = "Updates the data of an existing block.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Block ID to update", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated block data", Required = true)] BlockDto BlockDto)
        {
            return Response(await _BlockServices.UpdateAsync(id, BlockDto));
        }

        [HttpDelete("[action]/{id:int}")]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete block", Description = "Deletes a specific block by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("Block ID to delete", Required = true)] int id)
        {
            return Response(await _BlockServices.DeleteAsync(id));
        }
    }
}
