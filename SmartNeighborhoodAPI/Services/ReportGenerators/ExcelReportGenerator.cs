using System.Text;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Models;

namespace SmartNeighborhoodAPI.Services.ReportGenerators
{
    public class ExcelReportGenerator : IReportGenerator
    {
        private readonly ILogger<ExcelReportGenerator> _logger;

        public ExcelReportGenerator(ILogger<ExcelReportGenerator> logger)
        {
            _logger = logger;
        }

        public Task<byte[]> GenerateAsync(ReportParameters parameters)
        {
            _logger.LogInformation("Generating Excel report: '{Title}'", parameters.Title);
            var content = Encoding.UTF8.GetBytes($"[Excel] {parameters.Title} - Dummy Content");
            return Task.FromResult(content);
        }
    }
}
