using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.AppMetaData;
using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Helpers.DTOs.Teams;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{

    [SwaggerTag("Manage Families")]
    public class FamilyController : AppControllerBase
    {
        private readonly FamilyService _familyService;

        public FamilyController(FamilyService familyService)
        {
            _familyService = familyService;
        }

        [HttpPost(Router.Families.Add)]
        [SwaggerOperation(Summary = "Add a new family")]
        [ProducesResponseType(typeof(ReturnFamilyDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Family data to create a new family", Required = true)] FamilyDto familyDto)
        {
            var result = await _familyService.AddAsync(familyDto);
            return Response(result);
        }

        [HttpGet(Router.Families.GetAll)]
        [SwaggerOperation(Summary = "Get all families")]
        [ProducesResponseType(typeof(List<ReturnFamilyDto>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _familyService.GetAllAsync();
            return Response(result);
        }

        [HttpGet(Router.Families.GetDetails)]
        [SwaggerOperation(Summary = "Get full family details")]
        [ProducesResponseType(typeof(ReturnFamilyInfoDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetDetailesAsync([FromQuery, SwaggerParameter("ID of the family to retrieve", Required = true)] int id)
        {
            var result = await _familyService.GetDetailesAsync(id);
            return Response(result);
        }

        [HttpGet(Router.Families.GetById)]
        [SwaggerOperation(Summary = "Get family by ID")]
        [ProducesResponseType(typeof(ReturnFamilyDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("ID of the family to retrieve", Required = true)] int id)
        {
            var result = await _familyService.GetById(id);
            return Response(result);
        }

        [HttpPut(Router.Families.Update)]
        [SwaggerOperation(Summary = "Update a family")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 304)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("ID of the family to update", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated family data", Required = true)] FamilyDto familyDto)
        {
            var result = await _familyService.UpdateAsync(id, familyDto);
            return Response(result);
        }

        [HttpDelete(Router.Families.Delete)]
        [SwaggerOperation(Summary = "Delete a family")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 304)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("ID of the family to delete", Required = true)] int id)
        {
            var result = await _familyService.DeleteAsync(id);
            return Response(result);
        }
    }
}
