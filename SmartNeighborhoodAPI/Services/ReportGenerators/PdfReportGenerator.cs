using System.Text;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Models;

namespace SmartNeighborhoodAPI.Services.ReportGenerators
{
    public class PdfReportGenerator : IReportGenerator
    {
        private readonly ILogger<PdfReportGenerator> _logger;

        public PdfReportGenerator(ILogger<PdfReportGenerator> logger)
        {
            _logger = logger;
        }

        public Task<byte[]> GenerateAsync(ReportParameters parameters)
        {
            _logger.LogInformation("Generating PDF report: '{Title}'", parameters.Title);
            var content = Encoding.UTF8.GetBytes($"[PDF] {parameters.Title} - Dummy Content");
            return Task.FromResult(content);
        }
    }
}
