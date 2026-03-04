using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using SmartNeighborhoodAPI.Services;

namespace SmartNeighborhoodAPI.Controllers.V1
{

    [SwaggerTag("Project families management endpoints")]
    public class ProjectFamilyController : AppControllerBase
    {
        private readonly IProjectFamilieservice _projectFamilyService;

        public ProjectFamilyController(IProjectFamilieservice projectFamilyService)
        {
            _projectFamilyService = projectFamilyService;
        }

        [HttpGet(Router.ProjectFamilies.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all project families", Description = "Retrieves all project families.")]
        [ProducesResponseType(typeof(IEnumerable<ProjectFamilyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _projectFamilyService.GetAll());
        }

        [HttpGet(Router.ProjectFamilies.GetById)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get project family by ID", Description = "Retrieve a project family by its ID.")]
        [ProducesResponseType(typeof(ProjectFamilyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Project family ID", Required = true)] int id)
        {
            return Response(await _projectFamilyService.GetByIdAsync(id));
        }

        [HttpPost(Router.ProjectFamilies.Add)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Add a new project family", Description = "Adds a new project family.")]
        [ProducesResponseType(typeof(ProjectFamilyDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Project family data", Required = true)] ProjectFamilyDto dto)
        {
            return Response(await _projectFamilyService.AddAsync(dto));
        }

        [HttpPut(Router.ProjectFamilies.Update)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update project family", Description = "Updates an existing project family.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Project family ID", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated project family data", Required = true)] ProjectFamilyDto dto)
        {
            return Response(await _projectFamilyService.UpdateAsync(id, dto));
        }

        [HttpDelete(Router.ProjectFamilies.Delete)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete project family", Description = "Deletes a project family by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("Project family ID", Required = true)] int id)
        {
            return Response(await _projectFamilyService.DeleteAsync(id));
        }
    }
}
