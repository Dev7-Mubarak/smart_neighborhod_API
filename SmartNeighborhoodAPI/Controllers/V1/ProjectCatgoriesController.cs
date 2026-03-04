using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using SmartNeighborhoodAPI.Services;

namespace SmartNeighborhoodAPI.Controllers.V1
{

    [SwaggerTag("Project categories management endpoints")]
    public class ProjectCategoriesController : AppControllerBase
    {
        private readonly IProjectCatgoryService _projectCategoryService;

        public ProjectCategoriesController(IProjectCatgoryService projectCategoryService)
        {
            _projectCategoryService = projectCategoryService;
        }

        [HttpGet(Router.ProjectCategories.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all project categories", Description = "Retrieves all project categories.")]
        [ProducesResponseType(typeof(IEnumerable<ProjectCatgoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _projectCategoryService.GetAll());
        }

        [HttpGet(Router.ProjectCategories.GetById)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get project category by ID", Description = "Retrieve a project category by its ID.")]
        [ProducesResponseType(typeof(ProjectCatgoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Project category ID", Required = true)] int id)
        {
            return Response(await _projectCategoryService.GetByIdAsync(id));
        }

        [HttpPost(Router.ProjectCategories.Add)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Add a new project category", Description = "Adds a new project category.")]
        [ProducesResponseType(typeof(ProjectCatgoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Project category data", Required = true)] ProjectCatgoryDto dto)
        {
            return Response(await _projectCategoryService.AddAsync(dto));
        }

        [HttpPut(Router.ProjectCategories.Update)]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update project category", Description = "Updates an existing project category.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Project category ID", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated project category data", Required = true)] ProjectCatgoryDto dto)
        {
            return Response(await _projectCategoryService.UpdateAsync(id, dto));
        }

        [HttpDelete(Router.ProjectCategories.Delete)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete project category", Description = "Deletes a project category by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("Project category ID", Required = true)] int id)
        {
            return Response(await _projectCategoryService.DeleteAsync(id));
        }
    }
}
