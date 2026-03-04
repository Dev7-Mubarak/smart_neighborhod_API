using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.ConflictType;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Authorize]
    [SwaggerTag("Manage Conflict Case Types")]
    public class ConflictCaseTypeController : AppControllerBase
    {
        private readonly IConflictTypeService _conflictTypeService;

        public ConflictCaseTypeController(IConflictTypeService conflictTypeService)
        {
            _conflictTypeService = conflictTypeService;
        }

        [HttpPost(Router.ConflictCaseTypes.Add)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Add a new conflict case type", Description = "Creates a new conflict case type in the system.")]
        [ProducesResponseType(typeof(AddConflictTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddAsync(
            [FromBody, SwaggerParameter("Conflict type data to add", Required = true)] AddConflictTypeDto addConflictTypeDto)
        {
            var result = await _conflictTypeService.AddAsync(addConflictTypeDto);
            return Response(result);
        }

        [HttpGet(Router.ConflictCaseTypes.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get all conflict case types", Description = "Retrieves all conflict case types available in the system.")]
        // Returns 200 OK with an empty array when no types exist; 404 is never correct for a collection.
        [ProducesResponseType(typeof(IEnumerable<GetConflictTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _conflictTypeService.GetAll();
            return Response(result);
        }

        [HttpGet(Router.ConflictCaseTypes.GetById)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get conflict case type by ID", Description = "Retrieves a specific conflict case type by its ID.")]
        [ProducesResponseType(typeof(GetConflictTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute, SwaggerParameter("ID of the conflict type", Required = true)] int id)
        {
            var result = await _conflictTypeService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut(Router.ConflictCaseTypes.Update)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update a conflict case type", Description = "Updates the details of an existing conflict case type.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("ID of the conflict type to update", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated conflict type data", Required = true)] UpdateConflictTypeDto updateConflictTypeDto)
        {
            var result = await _conflictTypeService.UpdateAsync(id, updateConflictTypeDto);
            return Response(result);
        }

        [HttpDelete(Router.ConflictCaseTypes.Delete)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete a conflict case type", Description = "Deletes a specific conflict case type by its ID.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute, SwaggerParameter("ID of the conflict type to delete", Required = true)] int id)
        {
            var result = await _conflictTypeService.DeleteAsync(id);
            return Response(result);
        }
    }
}
