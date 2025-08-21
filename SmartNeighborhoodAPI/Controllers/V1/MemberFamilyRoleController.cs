using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs;
using OurProjectSmartNeiborhood.Services;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[EnableRateLimiting("fixed-window")]
    public class MemberFamilyRoleController : AppControllerBase
    {
        private readonly MemberFamilyRoleService _memberTypeService;

        public MemberFamilyRoleController(MemberFamilyRoleService memberTypeService)
        {
            _memberTypeService = memberTypeService;
        }

        [HttpPost("add-member-type")]
        public async Task<IActionResult> Add(string nameMemberType)
        {
            var result = await _memberTypeService.AddAsync(nameMemberType);
            return Response(result);
        }

        [HttpGet("get-all-member-types")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _memberTypeService.GetAll();
            return Response(result);
        }

        [HttpGet("get-member-type-by-id/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _memberTypeService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("update-member-type/{id:int}")]
        public async Task<IActionResult> Update(int id, string newNameMemberType)
        {
            var result = await _memberTypeService.UpdateAsync(id, newNameMemberType);
            return Response(result);
        }

        [HttpDelete("delete-member-type/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _memberTypeService.DeleteAsync(id);
            return Response(result);
        }
    }
}


