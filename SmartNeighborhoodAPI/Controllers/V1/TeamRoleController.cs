using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.AppMetaData;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{

    [SwaggerTag("Team roles management endpoints")]
    public class TeamRoleController : AppControllerBase
    {
        private readonly TeamRoleService _teamRoleService;

        public TeamRoleController(TeamRoleService teamRoleService)
        {
            _teamRoleService = teamRoleService;
        }

        [HttpGet(Router.TeamRoles.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all team roles", Description = "Retrieves all available team roles.")]
        [ProducesResponseType(typeof(IEnumerable<TeamRole>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _teamRoleService.GetAllAsync());
        }

        [HttpGet(Router.TeamRoles.GetById)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get team role by ID", Description = "Retrieve a team role by its ID.")]
        [ProducesResponseType(typeof(TeamRole), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Team role ID", Required = true)] int id)
        {
            return Response(await _teamRoleService.GetByIdAsync(id));
        }

        [HttpPost(Router.TeamRoles.Add)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Add a new team role", Description = "Adds a new team role.")]
        [ProducesResponseType(typeof(TeamRole), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Name of the team role", Required = true)] string teamRole)
        {
            return Response(await _teamRoleService.AddAsync(teamRole));
        }

        [HttpPut(Router.TeamRoles.Update)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Update team role", Description = "Updates an existing team role.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Team role ID", Required = true)] int id,
            [FromBody, SwaggerParameter("New name for the team role", Required = true)] string newRoleName)
        {
            return Response(await _teamRoleService.UpdateAsync(id, newRoleName));
        }

        [HttpDelete(Router.TeamRoles.Delete)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete team role", Description = "Deletes a team role by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("Team role ID", Required = true)] int id)
        {
            return Response(await _teamRoleService.DeleteAsync(id));
        }
    }
}
