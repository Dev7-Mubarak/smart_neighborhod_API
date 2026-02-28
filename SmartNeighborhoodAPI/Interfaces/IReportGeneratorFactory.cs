using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IReportGeneratorFactory
    {
        IReportGenerator Create(ReportFormat format);
    }
}
