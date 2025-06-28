using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;

namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : AppControllerBase
    {
        private readonly TeamMemberService _TeamMemberService;
       



        public TeamMembersController(TeamMemberService TeamMemberService)
        {
            _TeamMemberService = TeamMemberService;


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(AddTeamMemberDto dto)
        {
            var result = await _TeamMemberService.AddAsync(dto);
            return Response(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _TeamMemberService.GetAll();

            return Response(result);
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _TeamMemberService.GetByIdAsync(id);

            return Response(result);
        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, TeamMemberDto TeamMemberDto)
        {
            var result = await _TeamMemberService.UpdateAsync(id, TeamMemberDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _TeamMemberService.DeleteAsync(id);

            return Response(result);
        }
    }
}
