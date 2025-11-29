using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Entites.Enums;
using Swashbuckle.AspNetCore.Annotations;
using static SmartNeighborhoodAPI.Helpers.Router;

[SwaggerTag("ID Card Applications management endpoints")]
public class IDCardApplicationsController : AppControllerBase
{
    private readonly IIdCardApplicationService _service;

    public IDCardApplicationsController(IIdCardApplicationService service)
    {
        _service = service;
    }

    [HttpGet(IDCardApplications.GetAll)]
    [SwaggerOperation(Summary = "Retrieve all ID card applications", Description = "Supports filtering by ApplicationNumber and Status and pagination.")]
    [ProducesResponseType(typeof(PaginatedResult<GetAllIdCardApplicationDto>), 200)]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] string? applicationNumber,
        [FromQuery] ApplicationStatus? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await _service.GetAllAsync(applicationNumber, status, page, pageSize, ct);
        return Response(result);
    }

    [HttpGet(IDCardApplications.GetById)]
    [SwaggerOperation(Summary = "Get ID card application by ID")]
    [ProducesResponseType(typeof(GetIdCardApplicationDto), 200)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id) =>
        Response(await _service.GetByIdAsync(id));



    [HttpPost(IDCardApplications.Add)]
    [SwaggerOperation(Summary = "Add an ID card application")]
    [ProducesResponseType(typeof(GetIdCardApplicationDto), 201)]
    public async Task<IActionResult> AddAsync([FromBody] AddIdCardApplicationDto dto) =>
        Response(await _service.AddAsync(dto));

    [HttpPut(IDCardApplications.Update)]
    [SwaggerOperation(Summary = "Update ID card application")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateIdCardApplicationDto dto) =>
        Response(await _service.UpdateAsync(id, dto));

    [HttpDelete(IDCardApplications.Delete)]
    [SwaggerOperation(Summary = "Delete ID card application")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id) =>
        Response(await _service.DeleteAsync(id));
}
