using SmartNeighborhoodAPI.Models;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IReportGenerator
    {
        Task<byte[]> GenerateAsync(ReportParameters parameters);
    }
}
