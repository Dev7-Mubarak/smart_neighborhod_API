using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[ApiVersion("1.0")]
[SwaggerTag("Residential Neighborhoods management")]
public class ResidentialNeighborhoodsController : AppControllerBase
{
    private readonly IResidentialNeighborhoodService _service;

    public ResidentialNeighborhoodsController(IResidentialNeighborhoodService service)
    {
        _service = service;
    }

    [HttpGet(Router.ResidentialNeighborhoods.GetAll)]
    [SwaggerOperation(Summary = "Get all residential neighborhoods")]
    public async Task<IActionResult> GetAll()
        => Response(await _service.GetAllAsync());

    [HttpGet(Router.ResidentialNeighborhoods.GetById)]
    [SwaggerOperation(Summary = "Get residential neighborhood by ID")]
    public async Task<IActionResult> GetById(int id)
        => Response(await _service.GetByIdAsync(id));

    [HttpPost(Router.ResidentialNeighborhoods.Add)]
    [SwaggerOperation(Summary = "Create residential neighborhood")]
    public async Task<IActionResult> Create(CreateResidentialNeighborhoodDto dto)
        => Response(await _service.CreateAsync(dto));

    [HttpPut(Router.ResidentialNeighborhoods.Update)]
    [SwaggerOperation(Summary = "Update residential neighborhood")]
    public async Task<IActionResult> Update(int id, UpdateResidentialNeighborhoodDto dto)
        => Response(await _service.UpdateAsync(id, dto));

    [HttpDelete(Router.ResidentialNeighborhoods.Delete)]
    [SwaggerOperation(Summary = "Delete residential neighborhood")]
    public async Task<IActionResult> Delete(int id)
        => Response(await _service.DeleteAsync(id));
}
