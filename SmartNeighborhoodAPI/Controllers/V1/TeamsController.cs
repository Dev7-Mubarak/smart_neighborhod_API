using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[EnableRateLimiting("fixed-window")]
    public class TeamsController : AppControllerBase
    {
        private readonly TeamsService _TeamsService;

        public TeamsController(TeamsService TeamsService)
        {
            _TeamsService = TeamsService;


        }
        [HttpPost("get-all")]
        public async Task<IActionResult> AddAsync(TeamDto TeamDto)
        {
            var result = await _TeamsService.AddAsync(TeamDto);
            return Response(result);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _TeamsService.GetAll();

            return Response(result);
        }
        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _TeamsService.GetByIdAsync(id);

            return Response(result);

        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, TeamDto TeamDto)
        {
            var result = await _TeamsService.UpdateAsync(id, TeamDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _TeamsService.DeleteAsync(id);

            return Response(result);
        }

        [HttpGet("get-team-projects/{teamId:int}")]
        public async Task<IActionResult> GetTeamProjects(int teamId)
        {
            var result = await _TeamsService.GetTeamProjects(teamId);
            return Response(result);
        }
    }
}
