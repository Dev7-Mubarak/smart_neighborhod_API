using SmartNeighborhoodAPI.Helpers.DTOs.Project;

namespace SmartNeighborhoodAPI.Controllers
{
    public class ProjectsController : AppControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService ProjectService)
        {
            _projectService = ProjectService;


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(ProjectDto ProjectDto)
        {
            var result = await _projectService.AddAsync(ProjectDto);
            return Response(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync(int? ProjectCategoryId)
        {
            var result = await _projectService.GetAll(ProjectCategoryId);

            return Response(result);
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _projectService.GetByIdAsync(id);

            return Response(result);
        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, ProjectDto ProjectDto)
        {
            var result = await _projectService.UpdateAsync(id, ProjectDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _projectService.DeleteAsync(id);

            return Response(result);
        }

        [HttpPost("assign-team/{projectId:int}")]
        public async Task<IActionResult> AssignTeamToProject(int projectId, [FromQuery] int teamId)
        {
            var result = await _projectService.AssignTeamToProjectAsync(projectId, teamId);
            return Response(result);
        }

        [HttpDelete("remove-team/{projectId:int}")]
        public async Task<IActionResult> DeleteAssignTeamToProject(int projectId, [FromQuery] int teamId)
        {
            var result = await _projectService.DeleteAssignTeamFromProjectAsync(projectId, teamId);

            return Response(result);
        }

        [HttpPost("assign-family/{projectId:int}")]
        public async Task<IActionResult> AssignFamilyToProject(int projectId, [FromQuery] int familyId)
        {
            var result = await _projectService.AssignFamilyToProjectAsync(projectId, familyId);
            return Response(result);
        }

        [HttpDelete("remove-family/{projectId:int}")]
        public async Task<IActionResult> DeleteFamilyFromProject(int projectId, [FromQuery] int famileId)
        {
            var result = await _projectService.DeleteFamilyFromProjectAsync(projectId, famileId);

            return Response(result);
        }

        [HttpGet("GetProjectBlocksWithBeneficiaryFamilies/{projectId}")]
        public async Task<IActionResult> GetProjectBlocksWithBeneficiaryFamilies(int projectId)
        {
            var result = await _projectService.GetProjectBlocksWithBeneficiaryFamilies(projectId);
            return Response(result);
        }

        [HttpGet("GetProjectTeam/{projectId}")]
        public async Task<IActionResult> GetProjectTeam(int projectId)
        {
            var result = await _projectService.GetProjectTeam(projectId);
            return Response(result);
        }

    }


}
