using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using SmartNeighborhoodAPI.Helpers.DTOs;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Manage Family Types")]
    public class FamilyTypesController : AppControllerBase
    {
        private readonly FamilyTypeService _familyTypeService;

        public FamilyTypesController(FamilyTypeService familyTypeService)
        {
            _familyTypeService = familyTypeService;
        }

        [HttpPost("[action]")]
        [SwaggerOperation(Summary = "Add a new family type")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Name of the family type to add", Required = true)] string nameFamilyTypeDto)
        {
            var result = await _familyTypeService.AddAsync(nameFamilyTypeDto);
            return Response(result);
        }

        [HttpGet("get-all")]
        [SwaggerOperation(Summary = "Get all family types")]
        [ProducesResponseType(typeof(IEnumerable<GetFamilyTypeDto>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _familyTypeService.GetAll();
            return Response(result);
        }

        [HttpGet("get-by-id/{id:int}")]
        [SwaggerOperation(Summary = "Get a family type by ID")]
        [ProducesResponseType(typeof(GetFamilyTypeDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("ID of the family type", Required = true)] int id)
        {
            var result = await _familyTypeService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("[action]/{id:int}")]
        [SwaggerOperation(Summary = "Update a family type")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("ID of the family type to update", Required = true)] int id,
            [FromBody, SwaggerParameter("New name for the family type", Required = true)] string nameFamilyTypeDto)
        {
            var result = await _familyTypeService.UpdateAsync(id, nameFamilyTypeDto);
            return Response(result);
        }

        [HttpDelete("[action]/{id:int}")]
        [SwaggerOperation(Summary = "Delete a family type")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 304)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("ID of the family type to delete", Required = true)] int id)
        {
            var result = await _familyTypeService.DeleteAsync(id);
            return Response(result);
        }
    }
}
