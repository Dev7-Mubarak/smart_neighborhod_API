using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Entites.Enums;
using Swashbuckle.AspNetCore.Annotations;
using static SmartNeighborhoodAPI.Helpers.Router;

[SwaggerTag("Medicines management endpoints")]
public class MedicinesController : AppControllerBase
{
    private readonly IMedicineService _service;
    public MedicinesController(IMedicineService service) => _service = service;

    [HttpGet(Medicines.GetAll)]
    [SwaggerOperation(Summary = "Retrieve all medicines", Description = "Filter by name and type; supports pagination.")]
    [ProducesResponseType(typeof(PaginatedResult<GetAllMedicineDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] string? name, [FromQuery] MedicineType? type, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        return Response(await _service.GetAllAsync(name, type, page, pageSize, ct));
    }

    [HttpGet(Medicines.GetById)]
    [ProducesResponseType(typeof(GetMedicineDto), 200)]
    public async Task<IActionResult> GetById([FromRoute] int id) => Response(await _service.GetByIdAsync(id));

    [HttpPost(Medicines.Add)]
    [ProducesResponseType(typeof(GetMedicineDto), 201)]
    public async Task<IActionResult> Add([FromBody] AddMedicineDto dto) => Response(await _service.AddAsync(dto));

    [HttpPut(Medicines.Update)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMedicineDto dto) => Response(await _service.UpdateAsync(id, dto));

    [HttpDelete(Medicines.Delete)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Delete([FromRoute] int id) => Response(await _service.DeleteAsync(id));

    [HttpGet(Medicines.GetBatches)]
    [ProducesResponseType(typeof(List<GetMedicineBatchDto>), 200)]
    public async Task<IActionResult> GetBatches([FromRoute] int medicineId) => Response(await _service.GetBatchesByMedicineAsync(medicineId));

    [HttpPost(Medicines.AddBatch)]
    [ProducesResponseType(typeof(GetMedicineBatchDto), 201)]
    public async Task<IActionResult> AddBatch([FromRoute] int medicineId, [FromBody] AddMedicineBatchDto dto)
    {
        dto.MedicineId = medicineId;
        return Response(await _service.AddBatchAsync(dto));
    }

    [HttpPut(Medicines.UpdateBatch)]
    [ProducesResponseType(typeof(GetMedicineBatchDto), 200)]
    public async Task<IActionResult> UpdateBatch([FromRoute] int medicineId, [FromRoute] int batchId, [FromBody] UpdateMedicineBatchDto dto)
        => Response(await _service.UpdateBatchAsync(batchId, dto));

    [HttpDelete(Medicines.DeleteBatch)]
    public async Task<IActionResult> DeleteBatch([FromRoute] int medicineId, [FromRoute] int batchId)
        => Response(await _service.DeleteBatchAsync(batchId));
}
