using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;

namespace SmartNeighborhoodAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : AppControllerBase
    {
        private readonly ProjectService _ProjectService;

        public ProjectsController(ProjectService ProjectService)
        {
            _ProjectService = ProjectService;


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(ProjectDto ProjectDto)
        {
            var result = await _ProjectService.AddAsync(ProjectDto);
            return Response(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _ProjectService.GetAll();

            return Response(result);
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _ProjectService.GetByIdAsync(id);

            return Response(result);



        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, ProjectDto ProjectDto)
        {
            var result = await _ProjectService.UpdateAsync(id, ProjectDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _ProjectService.DeleteAsync(id);

            return Response(result);
        }
    }


}
