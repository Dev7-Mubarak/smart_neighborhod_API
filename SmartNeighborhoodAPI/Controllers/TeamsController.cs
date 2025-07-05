namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : AppControllerBase
    {
        private readonly TeamsService _TeamsService;

        public TeamsController(TeamsService TeamsService)
        {
            _TeamsService = TeamsService;


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(TeamDto TeamDto)
        {
            var result = await _TeamsService.AddAsync(TeamDto);
            return Response(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _TeamsService.GetAll();

            return Response(result);
        }
        [HttpGet("[action]/{id:int}")]
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

        [HttpGet("GetTeamProjects/{teamId:int}")]
        public async Task<IActionResult> GetTeamProjects(int teamId)
        {
            var result = await _TeamsService.GetTeamProjects(teamId);
            return Response(result);
        }
    }
}
