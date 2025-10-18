using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{

    [SwaggerTag("Projects management endpoints")]
    public class ProjectsController : AppControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet(Router.Projects.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all projects", Description = "Retrieves all projects optionally filtered by category.")]
        [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync([FromQuery, SwaggerParameter("Optional project category ID")] int? projectCategoryId)
        {
            return Response(await _projectService.GetAll(projectCategoryId));
        }

        [HttpGet(Router.Projects.GetById)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get project by ID", Description = "Retrieve a project by its ID.")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Project ID", Required = true)] int id)
        {
            return Response(await _projectService.GetProjectTeam(id));
        }

        [HttpPost(Router.Projects.Add)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Add a new project", Description = "Adds a new project.")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Project data", Required = true)] ProjectDto dto)
        {
            return Response(await _projectService.AddAsync(dto));
        }

        [HttpPut(Router.Projects.Update)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update project", Description = "Updates an existing project.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Project ID", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated project data", Required = true)] ProjectDto dto)
        {
            return Response(await _projectService.UpdateAsync(id, dto));
        }

        [HttpPut(Router.Projects.ChangeStatus)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Change project Status", Description = "Change an existing project status.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ChangeStatusAsync(
            [FromRoute, SwaggerParameter("Project ID", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated project status data", Required = true)] ProjectStatus projectStatus)
        {
            return Response(await _projectService.ChangeStatusAsync(id, projectStatus));
        }

        [HttpDelete(Router.Projects.Delete)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete project", Description = "Deletes a project by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("Project ID", Required = true)] int id)
        {
            return Response(await _projectService.DeleteAsync(id));
        }

        // Teams
        [HttpPost(Router.Projects.AssignTeam)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Assign team to project", Description = "Assigns a team to a project.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignTeamToProject(
            [FromRoute] int projectId,
            [FromRoute] int teamId)
        {
            return Response(await _projectService.AssignTeamToProjectAsync(projectId, teamId));
        }

        [HttpDelete(Router.Projects.RemoveTeam)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Remove team from project", Description = "Removes a team assignment from a project.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAssignTeamFromProject(
            [FromRoute] int projectId,
            [FromRoute] int teamId)
        {
            return Response(await _projectService.DeleteAssignTeamFromProjectAsync(projectId, teamId));
        }

        // Families
        [HttpPost(Router.Projects.AssignFamily)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Assign family to project", Description = "Assigns a family to a project.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignFamilyToProject(
            [FromRoute] int projectId,
            [FromRoute] int familyId) 
        {
            return Response(await _projectService.AssignFamilyToProjectAsync(projectId, familyId));
        }

        [HttpDelete(Router.Projects.RemoveFamily)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Remove family from project", Description = "Removes a family assignment from a project.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFamilyFromProject(
            [FromRoute] int projectId,
            [FromRoute] int familyId) 
        {
            return Response(await _projectService.DeleteFamilyFromProjectAsync(projectId, familyId));
        }

        [HttpGet(Router.Projects.GetProjectBlocksWithBeneficiaryFamilies)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get project blocks with beneficiary families", Description = "Retrieves all blocks related to a project with their beneficiary families.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectBlocksWithBeneficiaryFamilies(
            [FromRoute] int projectId)
        {
            return Response(await _projectService.GetProjectBlocksWithBeneficiaryFamilies(projectId));
        }

    }
}
