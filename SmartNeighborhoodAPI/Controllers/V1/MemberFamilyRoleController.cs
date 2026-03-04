using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace SmartNeighborhoodAPI.Controllers.V1
{

    [SwaggerTag("Manage Member Family Roles")]
    public class MemberFamilyRoleController : AppControllerBase
    {
        private readonly IMemberFamilyRoleService _memberTypeService;

        public MemberFamilyRoleController(IMemberFamilyRoleService memberTypeService)
        {
            _memberTypeService = memberTypeService;
        }

        [HttpPost(Router.MemberFamilyRoles.Add)]
        [SwaggerOperation(Summary = "Add a new member family role")]
        [ProducesResponseType(typeof(MemberFamilyRole), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Add(
            [FromBody, SwaggerParameter("Name of the member family role to add", Required = true)] string nameMemberType)
        {
            var result = await _memberTypeService.AddAsync(nameMemberType);
            return Response(result);
        }

        [HttpGet(Router.MemberFamilyRoles.GetAll)]
        [SwaggerOperation(Summary = "Get all member family roles")]
        [ProducesResponseType(typeof(IEnumerable<MemberFamilyRoleDto>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _memberTypeService.GetAll();
            return Response(result);
        }

        [HttpGet(Router.MemberFamilyRoles.GetById)]
        [SwaggerOperation(Summary = "Get a member family role by ID")]
        [ProducesResponseType(typeof(MemberFamilyRoleDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetById(
            [FromRoute, SwaggerParameter("ID of the member family role", Required = true)] int id)
        {
            var result = await _memberTypeService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut(Router.MemberFamilyRoles.Update)]
        [SwaggerOperation(Summary = "Update a member family role")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> Update(
            [FromRoute, SwaggerParameter("ID of the member family role to update", Required = true)] int id,
            [FromBody, SwaggerParameter("New name for the member family role", Required = true)] string newNameMemberType)
        {
            var result = await _memberTypeService.UpdateAsync(id, newNameMemberType);
            return Response(result);
        }

        [HttpDelete(Router.MemberFamilyRoles.Delete)]
        [SwaggerOperation(Summary = "Delete a member family role")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 304)]
        public async Task<IActionResult> Delete(
            [FromRoute, SwaggerParameter("ID of the member family role to delete", Required = true)] int id)
        {
            var result = await _memberTypeService.DeleteAsync(id);
            return Response(result);
        }
    }
}
