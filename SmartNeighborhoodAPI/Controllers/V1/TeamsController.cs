using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.AppMetaData;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{

    [SwaggerTag("Teams management endpoints")]
    [Route(Router.Teams.Prefix)]

    public class TeamsController : AppControllerBase
    {
        private readonly TeamsService _teamsService;

        public TeamsController(TeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpPost(Router.Teams.Add)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Add a new team", Description = "Creates a new team.")]
        [ProducesResponseType(typeof(TeamDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Team data to create", Required = true)] TeamDto teamDto)
        {
            return Response(await _teamsService.AddAsync(teamDto));
        }

        [HttpGet(Router.Teams.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all teams", Description = "Returns a list of all teams.")]
        [ProducesResponseType(typeof(IEnumerable<TeamDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _teamsService.GetAll());
        }

        [HttpGet(Router.Teams.GetById)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get team by ID", Description = "Retrieve a team by its ID.")]
        [ProducesResponseType(typeof(TeamDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Team ID to retrieve", Required = true)] int id)
        {
            return Response(await _teamsService.GetByIdAsync(id));
        }

        [HttpPut(Router.Teams.Update)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Update team", Description = "Updates an existing team.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Team ID to update", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated team data", Required = true)] TeamDto teamDto)
        {
            return Response(await _teamsService.UpdateAsync(id, teamDto));
        }

        [HttpDelete(Router.Teams.Delete)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete team", Description = "Deletes a specific team by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("Team ID to delete", Required = true)] int id)
        {
            return Response(await _teamsService.DeleteAsync(id));
        }

        [HttpGet(Router.Teams.GetTeamProjects)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get projects of a team", Description = "Returns all projects associated with a specific team.")]
        [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeamProjects([FromRoute, SwaggerParameter("Team ID to get projects", Required = true)] int teamId)
        {
            return Response(await _teamsService.GetTeamProjects(teamId));
        }
    }
}
