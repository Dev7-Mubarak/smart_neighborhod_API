using Microsoft.AspNetCore.Mvc;

namespace SmartNeighborhoodAPI.Controllers
{
    namespace SmartNeighborhoodAPI.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ProjectFamilyController : AppControllerBase
        {
            private readonly ProjectFamilieservice _ProjectFamilieservice;
            



            public ProjectFamilyController(ProjectFamilieservice ProjectFamilieservice)
            {
                _ProjectFamilieservice = ProjectFamilieservice;


            }
            [HttpPost("[action]")]
            public async Task<IActionResult> AddAsync(ProjectFamilyDto ProjectFamilyDto)
            {
                var result = await _ProjectFamilieservice.AddAsync(ProjectFamilyDto);
                return Response(result);
            }
            [HttpGet("[action]")]
            public async Task<IActionResult> GetAllAsync()
            {
                var result = await _ProjectFamilieservice.GetAll();

                return Response(result);
            }
            [HttpGet("[action]/{id:int}")]
            public async Task<IActionResult> GetByIdAsync(int id)
            {
                var result = await _ProjectFamilieservice.GetByIdAsync(id);

                return Response(result);



            }
            [HttpPut("[action]/{id:int}")]
            public async Task<IActionResult> UpdateAsync(int id, ProjectFamilyDto ProjectFamilyDto)
            {
                var result = await _ProjectFamilieservice.UpdateAsync(id, ProjectFamilyDto);

                return Response(result);
            }
            [HttpDelete("[action]/{id:int}")]
            public async Task<IActionResult> DeleteAsync(int id)
            {
                var result = await _ProjectFamilieservice.DeleteAsync(id);

                return Response(result);
            }
        }
    }

}
