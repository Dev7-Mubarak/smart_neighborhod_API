using Microsoft.AspNetCore.Authorization;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[EnableRateLimiting("fixed-window")]
    public class TeamRoleController : AppControllerBase
    {
        private readonly TeamRoleService _teamRoleService;

        public TeamRoleController(TeamRoleService teamRoleService)
        {
            _teamRoleService = teamRoleService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(string teamRole)
        {
            return Response(await _teamRoleService.AddAsync(teamRole));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _teamRoleService.GetAllAsync());
        }

        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Response(await _teamRoleService.GetByIdAsync(id));
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromBody] string newRoleName)
        {
            return Response(await _teamRoleService.UpdateAsync(id, newRoleName));
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Response(await _teamRoleService.DeleteAsync(id));
        }
    }
}
