using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using static SmartNeighborhoodAPI.Helpers.Router;

[SwaggerTag("Passport Applications management endpoints")]
public class PassportApplicationsController : AppControllerBase
{
    private readonly IPassportApplicationService _service;

    public PassportApplicationsController(IPassportApplicationService service)
    {
        _service = service;
    }

    [HttpGet(PassportApplications.GetAll)]
    [SwaggerOperation(Summary = "Retrieve all passport applications", Description = "Supports filtering by ApplicationNumber and Status and pagination.")]
    [ProducesResponseType(typeof(PaginatedResult<GetAllPassportApplicationDto>), 200)]
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

    [HttpGet(PassportApplications.GetById)]
    [SwaggerOperation(Summary = "Get passport application by ID")]
    [ProducesResponseType(typeof(GetPassportApplicationDto), 200)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id) =>
        Response(await _service.GetByIdAsync(id));


    [HttpPost(PassportApplications.Add)]
    [SwaggerOperation(Summary = "Add a passport application")]
    [ProducesResponseType(typeof(GetPassportApplicationDto), 201)]
    public async Task<IActionResult> AddAsync([FromBody] AddPassportApplicationDto dto) =>
        Response(await _service.AddAsync(dto));

    [HttpPut(PassportApplications.Update)]
    [SwaggerOperation(Summary = "Update passport application")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdatePassportApplicationDto dto) =>
        Response(await _service.UpdateAsync(id, dto));

    [HttpDelete(PassportApplications.Delete)]
    [SwaggerOperation(Summary = "Delete passport application")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id) =>
        Response(await _service.DeleteAsync(id));
}
