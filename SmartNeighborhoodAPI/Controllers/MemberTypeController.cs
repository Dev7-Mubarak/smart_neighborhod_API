using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs;
using OurProjectSmartNeiborhood.Services;

namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberTypeController : AppControllerBase
    {
        private readonly MemberTypeService _memberTypeService;

        public MemberTypeController(MemberTypeService memberTypeService)
        {
            _memberTypeService = memberTypeService;
        }

        [HttpPost("addMemberType")]
        public async Task<IActionResult> Add(string nameMemberType)
        {
            var result = await _memberTypeService.AddAsync(nameMemberType);
            return Response(result);
        }

        [HttpGet("getAllMemberTypes")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _memberTypeService.GetAll();
            return Response(result);
        }

        [HttpGet("getMemberTypeById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _memberTypeService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("updateMemberType/{id:int}")]
        public async Task<IActionResult> Update(int id, string newNameMemberType)
        {
            var result = await _memberTypeService.UpdateAsync(id, newNameMemberType);
            return Response(result);
        }

        [HttpDelete("deleteMemberType/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _memberTypeService.DeleteAsync(id);
            return Response(result);
        }
    }
}


