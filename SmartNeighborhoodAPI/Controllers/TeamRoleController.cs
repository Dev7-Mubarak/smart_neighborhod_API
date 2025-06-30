using Microsoft.AspNetCore.Authorization;

namespace SmartNeighborhoodAPI.Controllers
{
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _teamRoleService.GetAllAsync());
        }

        [HttpGet("[action]/{id:int}")]
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
