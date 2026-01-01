using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;
using static SmartNeighborhoodAPI.Helpers.Router;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Authorize(Roles = Role.Admin)]
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Residential Neighborhoods management")]
    public class ResidentialNeighborhoodsController : ControllerBase
    {
        private readonly IResidentialNeighborhoodService _service;

        public ResidentialNeighborhoodsController(
            IResidentialNeighborhoodService service)
        {
            _service = service;
        }

        private IActionResult Response<T>(ApiResponse<T> response)
        {
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet(ResidentialNeighborhoods.GetAll)]
        [SwaggerOperation(Summary = "Get all residential neighborhoods (Admin only)")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
            => Response(await _service.GetAllAsync(ct));

        [HttpGet(ResidentialNeighborhoods.GetById)]
        [SwaggerOperation(Summary = "Get residential neighborhood by ID (Admin only)")]
        public async Task<IActionResult> GetById(int id)
            => Response(await _service.GetByIdAsync(id));

        [HttpPost(ResidentialNeighborhoods.Add)]
        [SwaggerOperation(Summary = "Create residential neighborhood (Admin only)")]
        public async Task<IActionResult> Create(
            [FromBody] CreateResidentialNeighborhoodDto dto)
            => Response(await _service.CreateAsync(dto));

        [HttpPut(ResidentialNeighborhoods.Update)]
        [SwaggerOperation(Summary = "Update residential neighborhood (Admin only)")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateResidentialNeighborhoodDto dto)
            => Response(await _service.UpdateAsync(id, dto));



        [HttpPost(ResidentialNeighborhoods.ChangeManager)]
        [SwaggerOperation(Summary = "Change residential neighborhood manager (Admin only)")]
        public async Task<IActionResult> ChangeManager(
            int id,
            [FromBody] ChangeResidentialManagerDto dto)
            => Response(await _service.ChangeManagerAsync(dto));

        [HttpGet(ResidentialNeighborhoods.Dashboard)]
        [SwaggerOperation(Summary = "Get residential neighborhood dashboard statistics (Admin only)")]
        public async Task<IActionResult> GetDashboard(CancellationToken ct)
            => Response(await _service.GetDashboardAsync(ct));
    }
}
