using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using static SmartNeighborhoodAPI.Helpers.Router;

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

    [HttpGet(ResidentialNeighborhoods.GetAll)]
    [SwaggerOperation(
          Summary = "Get all residential neighborhoods",
          Description = "Returns paginated residential neighborhoods with optional filtering by name and manager.")]
    [ProducesResponseType(typeof(PaginatedResult<ReturnResidentialNeighborhoodDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAsync(
          [FromQuery] string? name,
          [FromQuery] string? managerId,
          [FromQuery] int page = 1,
          [FromQuery] int pageSize = 10,
          CancellationToken ct = default)
    {
        var result = await _service.GetAllAsync(
            name,
            managerId,
            page,
            pageSize,
            ct);

        return Response(result);
    }

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
    [HttpGet(Router.ResidentialNeighborhoods.Search)]
    public async Task<IActionResult> Search(
        [FromQuery] string keyword,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        return Response(await _service.SearchAsync(keyword, page, pageSize));
    }

    [HttpGet(Router.ResidentialNeighborhoods.Dashboard)]
    [SwaggerOperation(Summary = "Get residential dashboard statistics")]
    [ProducesResponseType(typeof(ResidentialDashboardDto), 200)]
    public async Task<IActionResult> Dashboard()
    {
        return Response(await _service.GetDashboardAsync());
    }
}
