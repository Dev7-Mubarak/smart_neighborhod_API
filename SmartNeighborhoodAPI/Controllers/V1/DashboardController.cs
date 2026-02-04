using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Dashboard;
using SmartNeighborhoodAPI.Interfaces;
using System.Threading.Tasks;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    public class DashboardController : AppControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats([FromQuery(Name = "neighborhood_id")] int? neighborhoodId, [FromQuery(Name = "residentialUnit_id")] int? residentialUnitId)
        {
            var stats = await _dashboardService.GetDashboardStatsAsync(neighborhoodId, residentialUnitId);
            return Response(ApiResponse<DashboardStatsDto>.Success(stats));
        }
    }
}
