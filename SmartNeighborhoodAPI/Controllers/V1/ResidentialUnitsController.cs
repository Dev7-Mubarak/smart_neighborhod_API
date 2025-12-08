using Microsoft.AspNetCore.Authorization;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Claims;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [SwaggerTag("Residential Units management endpoints")]
    [Authorize(Roles = Role.Admin + "," + Role.UnitManager)]
    public class ResidentialUnitsController : AppControllerBase
    {
        private readonly IResidentialUnitService _unitServices;

        public ResidentialUnitsController(IResidentialUnitService unitServices)
        {
            _unitServices = unitServices;
        }

        [HttpGet(Router.ResidentialUnits.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get all residential units", Description = "Returns all units accessible by the user.")]
        [ProducesResponseType(typeof(IEnumerable<ReturnResidentialUnitDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync(
        [FromQuery] string? name,
        [FromQuery] string? managerId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
        {
            var result = await _unitServices.GetAllAsync(name, managerId, page, pageSize, ct);
            return Response(result);
        }

        [HttpGet(Router.ResidentialUnits.GetById)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get unit by ID", Description = "Retrieve a unit by its ID.")]
        [ProducesResponseType(typeof(ReturnResidentialUnitDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Unit ID to retrieve", Required = true)] int id)
        {
            return Response(await _unitServices.GetByIdAsync(id));
        }

        [HttpPost(Router.ResidentialUnits.Add)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Create a new residential unit", Description = "Creates a new unit and returns the created entity.")]
        [ProducesResponseType(typeof(ReturnResidentialUnitDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Unit data to create", Required = true)] AddResidentialUnitDto unitDto)
        {
            return Response(await _unitServices.AddAsync(unitDto));
        }

        [HttpPut(Router.ResidentialUnits.Update)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update residential unit", Description = "Updates an existing unit.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Unit ID to update", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated unit data", Required = true)] UpdateResidentialUnitDto unitDto)
        {
            return Response(await _unitServices.UpdateAsync(id, unitDto));
        }

        [HttpDelete(Router.ResidentialUnits.Delete)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete residential unit", Description = "Deletes a specific unit by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("Unit ID to delete", Required = true)] int id)
        {
            return Response(await _unitServices.DeleteAsync(id));
        }

    }
}
