using Microsoft.AspNetCore.Authorization;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs;
using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Authorize]
    [MapToApiVersion("1.0")]
    [SwaggerTag("Manage Conflict Cases")]
    public class ConflictCaseController : AppControllerBase
    {
        private readonly IConflictCaseService _conflictCaseService;

        public ConflictCaseController(IConflictCaseService conflictCaseService)
        {
            _conflictCaseService = conflictCaseService;
        }

        [HttpPost(Router.ConflictCases.Add)]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(
            Summary = "Add a new conflict case",
            Description = "Creates a new conflict case with an optional image and assigned manager.")]
        [ProducesResponseType(typeof(ReturnConflictCaseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddAsync(
            [FromForm, SwaggerParameter("Conflict case data to add", Required = true)] AddConflictCaseDto conflictCaseDto)
        {
            var result = await _conflictCaseService.AddAsync(conflictCaseDto);
            return Response(result);
        }

        [HttpGet(Router.ConflictCases.GetAll)]
        [SwaggerOperation(
            Summary = "Get all conflict cases",
            Description = "Retrieves a paginated, filterable list of conflict cases. All query parameters are optional.")]
        [ProducesResponseType(typeof(PaginatedResult<GetConflictCaseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] ConflictCaseFilterParams filter)
        {
            var result = await _conflictCaseService.GetAllAsync(filter);
            return Response(result);
        }

        [HttpGet(Router.ConflictCases.GetById)]
        [SwaggerOperation(Summary = "Get conflict case by ID", Description = "Retrieves a specific conflict case by its ID.")]
        [ProducesResponseType(typeof(GetConflictCaseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute, SwaggerParameter("ID of the conflict case", Required = true)] int id)
        {
            var result = await _conflictCaseService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut(Router.ConflictCases.Update)]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Update a conflict case", Description = "Updates an existing conflict case, including optional image and manager replacement.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("ID of the conflict case to update", Required = true)] int id,
            [FromForm, SwaggerParameter("Updated conflict case data", Required = true)] UpdateConflictCaseDto conflictCaseDto)
        {
            var result = await _conflictCaseService.UpdateAsync(id, conflictCaseDto);
            return Response(result);
        }

        [HttpDelete(Router.ConflictCases.Delete)]
        [SwaggerOperation(Summary = "Delete a conflict case", Description = "Deletes a specific conflict case by its ID.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute, SwaggerParameter("ID of the conflict case to delete", Required = true)] int id)
        {
            var result = await _conflictCaseService.DeleteAsync(id);
            return Response(result);
        }

        [HttpGet(Router.ConflictCases.GetByFamilyMember)]
        [SwaggerOperation(
            Summary = "Get conflict cases by family member",
            Description = "Retrieves all conflict cases where the specified family member is a party.")]
        [ProducesResponseType(typeof(IEnumerable<GetConflictCaseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByFamilyMemberId(
            [FromRoute, SwaggerParameter("ID of the family member", Required = true)] int familyMemberId)
        {
            var result = await _conflictCaseService.GetByFamilyMemberIdAsync(familyMemberId);
            return Response(result);
        }
    }
}
