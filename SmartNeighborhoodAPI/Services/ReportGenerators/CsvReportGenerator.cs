using System.Text;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Models;

namespace SmartNeighborhoodAPI.Services.ReportGenerators
{
    public class CsvReportGenerator : IReportGenerator
    {
        private readonly ILogger<CsvReportGenerator> _logger;

        public CsvReportGenerator(ILogger<CsvReportGenerator> logger)
        {
            _logger = logger;
        }

        public Task<byte[]> GenerateAsync(ReportParameters parameters)
        {
            _logger.LogInformation("Generating CSV report: '{Title}'", parameters.Title);
            var content = Encoding.UTF8.GetBytes($"[CSV] {parameters.Title} - Dummy Content");
            return Task.FromResult(content);
        }
    }
}
