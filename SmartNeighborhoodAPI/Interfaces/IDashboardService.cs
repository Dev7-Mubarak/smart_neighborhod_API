using System.Threading.Tasks;
using SmartNeighborhoodAPI.Helpers.DTOs.Dashboard;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync(int? neighborhoodId = null, int? residentialUnitId = null);
    }
}
