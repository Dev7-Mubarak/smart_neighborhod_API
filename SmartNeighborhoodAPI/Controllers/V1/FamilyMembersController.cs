using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Manage Family Members")]
    public class FamilyMembersController : AppControllerBase
    {
        private readonly FamilyMemberService _familyMemberService;

        public FamilyMembersController(FamilyMemberService familyMemberService)
        {
            _familyMemberService = familyMemberService;
        }

        [HttpPost("[action]")]
        [SwaggerOperation(Summary = "Add a new family member")]
        [ProducesResponseType(typeof(FamilyMember), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> AddAsync([FromBody, SwaggerParameter("Family member data to add", Required = true)] FamilyMemberDto familyMemberDto)
        {
            var result = await _familyMemberService.AddAsync(familyMemberDto);
            return Response(result);
        }

        [HttpGet("get-all")]
        [SwaggerOperation(Summary = "Get all family members")]
        [ProducesResponseType(typeof(IEnumerable<ReturnFamilyMemberWithFullInfo>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _familyMemberService.GetAllAsync();
            return Response(result);
        }

        [HttpGet("get-by-id/{id:int}")]
        [SwaggerOperation(Summary = "Get a family member by ID")]
        [ProducesResponseType(typeof(ReturnFamilyMemberWithFullInfo), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("ID of the family member to retrieve", Required = true)] int id)
        {
            var result = await _familyMemberService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("[action]/{id:int}")]
        [SwaggerOperation(Summary = "Update a family member")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("ID of the family member to update", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated family member data", Required = true)] FamilyMember familyMember)
        {
            var result = await _familyMemberService.UpdateAsync(id, familyMember);
            return Response(result);
        }

        [HttpDelete("[action]/{id:int}")]
        [SwaggerOperation(Summary = "Delete a family member from a family")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 304)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute, SwaggerParameter("ID of the family member to delete", Required = true)] int id,
            [FromQuery, SwaggerParameter("ID of the family", Required = true)] int familyId)
        {
            var result = await _familyMemberService.DeleteAsync(id, familyId);
            return Response(result);
        }
    }
}
