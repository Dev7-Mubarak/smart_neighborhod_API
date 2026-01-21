using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;
using static SmartNeighborhoodAPI.Helpers.Router;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Residential Neighborhoods management")]
    public class ResidentialNeighborhoodsController : AppControllerBase
    {
        private readonly IResidentialNeighborhoodService _service;
        private readonly UserManager<AppUser> _userManager;

        public ResidentialNeighborhoodsController(
            IResidentialNeighborhoodService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
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
        [Authorize(Roles = Role.Admin)]
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
        [Authorize(Roles = Role.Admin)]
        [SwaggerOperation(Summary = "Change residential neighborhood manager (Admin only)")]
        public async Task<IActionResult> ChangeManager(
            int id,
            [FromBody] ChangeResidentialManagerDto dto)
            => Response(await _service.ChangeManagerAsync(id, dto));

        [HttpGet(ResidentialNeighborhoods.Dashboard)]
        [Authorize]
        [SwaggerOperation(Summary = "Get residential neighborhood dashboard statistics (Admin or Manager)")]
        [ProducesResponseType(typeof(ResidentialDashboardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ResidentialNeighborhoodManagerDashboardDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetDashboard(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Unauthorized();

            if (await _userManager.IsInRoleAsync(user, Role.Admin))
            {
                return Response(await _service.GetDashboardAsync(ct));
            }

            if (await _userManager.IsInRoleAsync(user, Role.ResidentialNeighborhoodManager))
            {
                return Response(await _service.GetMyDashboardAsync(userId, ct));
            }

            return Forbid();
        }

        [HttpGet(ResidentialNeighborhoods.Units)]
        [SwaggerOperation(Summary = "Get units (Admin only)")]
        public async Task<IActionResult> GetUnitsAsync(int id)
            => Response(await _service.GetUnitsAsync(id));

        [HttpGet(ResidentialNeighborhoods.GetMyDashboard)]
        [SwaggerOperation(
            Summary = "Get my dashboard statistics (Manager only)",
            Description = "Returns dashboard statistics for the authenticated residential neighborhood manager, including total neighborhoods, units, and blocks they manage.")]
        [ProducesResponseType(typeof(ApiResponse<ResidentialNeighborhoodManagerDashboardDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetMyDashboard(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Response(await _service.GetMyDashboardAsync(userId, ct));
        }

        [HttpGet(ResidentialNeighborhoods.GetMyNeighborhoods)]
        [SwaggerOperation(
            Summary = "Get my managed neighborhoods (Manager only)",
            Description = "Returns all residential neighborhoods managed by the authenticated user, including residential units and blocks summary.")]
        [ProducesResponseType(typeof(ApiResponse<List<ReturnResidentialUnitDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetMyNeighborhoods(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Response(await _service.GetMyNeighborhoodsAsync(userId, ct));
        }
    }
}
