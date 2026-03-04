using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{

    [SwaggerTag("Team members management endpoints")]
    public class TeamMembersController : AppControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMembersController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        [HttpGet(Router.TeamMembers.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all team members", Description = "Retrieves all team members.")]
        [ProducesResponseType(typeof(IEnumerable<TeamMemberDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _teamMemberService.GetAll());
        }

        [HttpGet(Router.TeamMembers.GetById)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get team member by ID", Description = "Retrieve a team member by its ID.")]
        [ProducesResponseType(typeof(TeamMemberDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Team member ID", Required = true)] int id)
        {
            return Response(await _teamMemberService.GetByIdAsync(id));
        }

        [HttpPost(Router.TeamMembers.Add)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Add a new team member", Description = "Adds a new team member.")]
        [ProducesResponseType(typeof(TeamMemberDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Team member data", Required = true)] AddTeamMemberDto dto)
        {
            return Response(await _teamMemberService.AddAsync(dto));
        }

        [HttpPut(Router.TeamMembers.Update)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update team member", Description = "Updates an existing team member.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Team member ID", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated team member data", Required = true)] UpdateTeamMemberDto dto)
        {
            return Response(await _teamMemberService.UpdateAsync(id, dto));
        }

        [HttpDelete(Router.TeamMembers.Delete)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete team member", Description = "Deletes a team member by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("Team member ID", Required = true)] int id)
        {
            return Response(await _teamMemberService.DeleteAsync(id));
        }
    }
}
