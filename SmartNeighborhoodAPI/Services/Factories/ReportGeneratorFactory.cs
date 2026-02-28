using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Services.ReportGenerators;

namespace SmartNeighborhoodAPI.Services.Factories
{
    public class ReportGeneratorFactory : IReportGeneratorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportGeneratorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IReportGenerator Create(ReportFormat format) => format switch
        {
            ReportFormat.Pdf => _serviceProvider.GetRequiredService<PdfReportGenerator>(),
            ReportFormat.Excel => _serviceProvider.GetRequiredService<ExcelReportGenerator>(),
            ReportFormat.Csv => _serviceProvider.GetRequiredService<CsvReportGenerator>(),
            _ => throw new ArgumentOutOfRangeException(nameof(format), format,
                     $"No report generator is registered for format '{format}'.")
        };
    }
}
