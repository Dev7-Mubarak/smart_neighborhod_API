using System.Threading.Tasks;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.DTOs.Dashboard;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync(int? neighborhoodId = null, int? residentialUnitId = null);

        Task<byte[]> ExportDashboardStatsAsync(ReportFormat format, int? neighborhoodId = null, int? residentialUnitId = null);
    }
}
