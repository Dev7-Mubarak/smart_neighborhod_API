using Microsoft.Extensions.Logging;
using Moq;
using SmartNeighborhoodAPI.Models;
using SmartNeighborhoodAPI.Services.ReportGenerators;

namespace SmartNeighborhoodAPI.Tests.Services.ReportGenerators;

public class PdfReportGeneratorTests
{
    private readonly PdfReportGenerator _generator;

    public PdfReportGeneratorTests()
    {
        _generator = new PdfReportGenerator(Mock.Of<ILogger<PdfReportGenerator>>());
    }

    [Fact]
    public async Task GenerateAsync_WithValidParameters_ReturnsNonNullByteArray()
    {
        // Arrange
        var parameters = new ReportParameters
        {
            Title = "Test Neighborhood Report",
            Data = new { TotalFamilies = 10 }
        };

        // Act
        var result = await _generator.GenerateAsync(parameters);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GenerateAsync_WithValidParameters_ContentContainsPdfPrefix()
    {
        // Arrange
        var parameters = new ReportParameters { Title = "My Report", Data = null };

        // Act
        var result = await _generator.GenerateAsync(parameters);
        var content = System.Text.Encoding.UTF8.GetString(result);

        // Assert
        Assert.Contains("[PDF]", content);
        Assert.Contains("My Report", content);
    }
}
