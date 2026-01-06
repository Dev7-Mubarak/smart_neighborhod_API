using Microsoft.AspNetCore.Authorization;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Claims;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [SwaggerTag("Residential Units management endpoints")]
    [Authorize(Roles = Role.Admin + "," + Role.UnitManager)]
    public class ResidentialUnitsController : AppControllerBase
    {
        private readonly ResidentialUnitService _unitServices;

        public ResidentialUnitsController(ResidentialUnitService unitServices)
        {
            _unitServices = unitServices;
        }

        [HttpGet(Router.ResidentialUnits.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get all residential units", Description = "Returns all units accessible by the user.")]
        [ProducesResponseType(typeof(IEnumerable<ReturnResidentialUnitDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _unitServices.GetAllAsync());
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

        [HttpPost(Router.ResidentialUnits.ChangeManager)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Change residential unit manager", Description = "Changes the manager of a residential unit.")]
        [ProducesResponseType(typeof(ReturnResidentialUnitDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeManagerAsync([FromBody, SwaggerParameter("Manager change details", Required = true)] ChangeResidentialUnitManagerDto dto)
        {
            return Response(await _unitServices.ChangeManagerAsync(dto));
        }

        [HttpGet(Router.ResidentialUnits.Dashboard)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get residential unit dashboard", Description = "Returns dashboard statistics for residential units.")]
        [ProducesResponseType(typeof(ResidentialUnitDashboardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardAsync(CancellationToken ct = default)
        {
            return Response(await _unitServices.GetDashboardAsync(ct));
        }

    }
}
