using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[EnableRateLimiting("fixed-window")]

    public class FamilyMembersController : AppControllerBase
    {
        private readonly FamilyMemberService _familyMemberService;

        public FamilyMembersController(FamilyMemberService familyMemberService)
        {
            _familyMemberService = familyMemberService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(FamilyMemberDto familyMemberDto)
        {
            var result = await _familyMemberService.AddAsync(familyMemberDto);
            return Response(result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _familyMemberService.GetAllAsync();
            return Response(result);
        }

        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _familyMemberService.GetByIdAsync(id);
            return Response(result);
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, FamilyMember familyMember)
        {
            var result = await _familyMemberService.UpdateAsync(id,familyMember);
            return Response(result);
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, [FromQuery] int familyId)
        {
            var result = await _familyMemberService.DeleteAsync(id, familyId);
            return Response(result);
        }

    }
}
