using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartNeighborhoodAPI.Controllers
{

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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _familyMemberService.GetAll();
            return Response(result);
        }

        [HttpGet("[action]/{id:int}")]
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _familyMemberService.DeleteAsync(id);
            return Response(result);
        }
    }
}
