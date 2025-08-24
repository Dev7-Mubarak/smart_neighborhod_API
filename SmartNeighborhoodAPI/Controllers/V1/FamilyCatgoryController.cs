using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.AppMetaData;
using SmartNeighborhoodAPI.Helpers.DTOs;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [SwaggerTag("Manage Family Categories")]
    [Route(Router.FamilyCategories.Prefix)]

    public class FamilyCatgoryController : AppControllerBase
    {
        private readonly FamilyCatgoryService _familyCatgoryService;

        public FamilyCatgoryController(FamilyCatgoryService familyCatgoryService)
        {
            _familyCatgoryService = familyCatgoryService;
        }

        [HttpPost(Router.FamilyCategories.Add)]
        [SwaggerOperation(Summary = "Add a new family category")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Name of the new family category", Required = true)] string name)
        {
            var result = await _familyCatgoryService.AddAsync(name);
            return Response(result);
        }

        [HttpGet(Router.FamilyCategories.GetAll)]
        [SwaggerOperation(Summary = "Get all family categories")]
        [ProducesResponseType(typeof(IEnumerable<FamilyCatgoryDto>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _familyCatgoryService.GetAll();
            return Response(result);
        }

        [HttpGet(Router.FamilyCategories.GetById)]
        [SwaggerOperation(Summary = "Get family category by ID")]
        [ProducesResponseType(typeof(FamilyCatgoryDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _familyCatgoryService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut(Router.FamilyCategories.Update)]
        [SwaggerOperation(Summary = "Update a family category")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody, SwaggerParameter("New name for the family category", Required = true)] string name)
        {
            var result = await _familyCatgoryService.UpdateAsync(id, name);
            return Response(result);
        }

        [HttpDelete(Router.FamilyCategories.Delete)]
        [SwaggerOperation(Summary = "Delete a family category")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _familyCatgoryService.DeleteAsync(id);
            return Response(result);
        }
    }
}
