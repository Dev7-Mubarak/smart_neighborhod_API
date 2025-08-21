using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs;
using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Manage Conflict Cases")]
    public class ConflictCaseController : AppControllerBase
    {
        private readonly ConflictCaseService _conflictCaseService;

        public ConflictCaseController(ConflictCaseService conflictCaseService)
        {
            _conflictCaseService = conflictCaseService;
        }

        [HttpPost("[action]")]
        [SwaggerOperation(Summary = "Add a new conflict case", Description = "Creates a new conflict case with optional image and manager.")]
        [ProducesResponseType(typeof(ReturnConflictCaseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Conflict case data to add", Required = true)] AddConflictCaseDto conflictCaseDto)
        {
            var result = await _conflictCaseService.AddAsync(conflictCaseDto);
            return Response(result);
        }

        [HttpGet("get-all")]
        [SwaggerOperation(Summary = "Get all conflict cases", Description = "Retrieves all conflict cases in the system.")]
        [ProducesResponseType(typeof(IEnumerable<GetConflictCaseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _conflictCaseService.GetAll();
            return Response(result);
        }

        [HttpGet("get-by-id/{id:int}")]
        [SwaggerOperation(Summary = "Get conflict case by ID", Description = "Retrieves a specific conflict case by its ID.")]
        [ProducesResponseType(typeof(GetConflictCaseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("ID of the conflict case", Required = true)] int id)
        {
            var result = await _conflictCaseService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("[action]/{id:int}")]
        [SwaggerOperation(Summary = "Update a conflict case", Description = "Updates an existing conflict case, including image and manager.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("ID of the conflict case to update", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated conflict case data", Required = true)] UpdateConflictCaseDto conflictCaseDto)
        {
            var result = await _conflictCaseService.UpdateAsync(id, conflictCaseDto);
            return Response(result);
        }

        [HttpDelete("[action]/{id:int}")]
        [SwaggerOperation(Summary = "Delete a conflict case", Description = "Deletes a specific conflict case by its ID.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("ID of the conflict case to delete", Required = true)] int id)
        {
            var result = await _conflictCaseService.DeleteAsync(id);
            return Response(result);
        }

        [HttpGet("get-by-family-member/{familyMemberId}")]
        [SwaggerOperation(Summary = "Get conflict cases by family member", Description = "Retrieves all conflict cases where the specified family member is involved.")]
        [ProducesResponseType(typeof(IEnumerable<GetConflictCaseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByFamilyMemberId([FromRoute, SwaggerParameter("ID of the family member", Required = true)] int familyMemberId)
        {
            var result = await _conflictCaseService.GetByFamilyMemberIdAsync(familyMemberId);
            return Response(result);
        }
    }
}
