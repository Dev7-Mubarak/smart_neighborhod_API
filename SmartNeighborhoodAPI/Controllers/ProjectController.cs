using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Services;

namespace SmartNeighborhoodAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost("assign-family/{projectId:int}")]
        public async Task<IActionResult> AssignFamilyToProject(int projectId, [FromQuery] int familyId)
        {
            var result = await _projectService.AssignFamilyToProjectAsync(projectId, familyId);
            return Response(result);
        }


    }


}
