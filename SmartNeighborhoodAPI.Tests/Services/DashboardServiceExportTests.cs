using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Models;
using SmartNeighborhoodAPI.Services;

namespace SmartNeighborhoodAPI.Tests.Services;

public class DashboardServiceExportTests
{
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task ExportDashboardStatsAsync_CallsFactoryCreate_AndGeneratorGenerateAsync()
    {
        // Arrange
        var expectedBytes = System.Text.Encoding.UTF8.GetBytes("Dashboard PDF Content");

        var mockGenerator = new Mock<IReportGenerator>();
        mockGenerator
            .Setup(g => g.GenerateAsync(It.IsAny<ReportParameters>()))
            .ReturnsAsync(expectedBytes);

        var mockFactory = new Mock<IReportGeneratorFactory>();
        mockFactory
            .Setup(f => f.Create(ReportFormat.Pdf))
            .Returns(mockGenerator.Object);

        using var context = CreateInMemoryContext();
        var service = new DashboardService(context, mockFactory.Object);

        // Act
        var result = await service.ExportDashboardStatsAsync(ReportFormat.Pdf);

        // Assert — factory was called with the correct format
        mockFactory.Verify(f => f.Create(ReportFormat.Pdf), Times.Once);

        // Assert — generator's GenerateAsync was called
        mockGenerator.Verify(g => g.GenerateAsync(It.IsAny<ReportParameters>()), Times.Once);

        // Assert — returned bytes match expectation
        Assert.Equal(expectedBytes, result);
    }

    [Fact]
    public async Task ExportDashboardStatsAsync_PassesCorrectTitleToParameters()
    {
        // Arrange
        ReportParameters? capturedParams = null;
        var mockGenerator = new Mock<IReportGenerator>();
        mockGenerator
            .Setup(g => g.GenerateAsync(It.IsAny<ReportParameters>()))
            .Callback<ReportParameters>(p => capturedParams = p)
            .ReturnsAsync(Array.Empty<byte>());

        var mockFactory = new Mock<IReportGeneratorFactory>();
        mockFactory
            .Setup(f => f.Create(It.IsAny<ReportFormat>()))
            .Returns(mockGenerator.Object);

        using var context = CreateInMemoryContext();
        var service = new DashboardService(context, mockFactory.Object);

        // Act
        await service.ExportDashboardStatsAsync(ReportFormat.Excel, neighborhoodId: 7);

        // Assert — title contains neighborhood id
        Assert.NotNull(capturedParams);
        Assert.Contains("7", capturedParams!.Title);
    }

    [Fact]
    public async Task ExportDashboardStatsAsync_WithCsvFormat_InvokesCorrectGenerator()
    {
        // Arrange
        var mockGenerator = new Mock<IReportGenerator>();
        mockGenerator
            .Setup(g => g.GenerateAsync(It.IsAny<ReportParameters>()))
            .ReturnsAsync(Array.Empty<byte>());

        var mockFactory = new Mock<IReportGeneratorFactory>();
        mockFactory
            .Setup(f => f.Create(ReportFormat.Csv))
            .Returns(mockGenerator.Object);

        using var context = CreateInMemoryContext();
        var service = new DashboardService(context, mockFactory.Object);

        // Act
        await service.ExportDashboardStatsAsync(ReportFormat.Csv);

        // Assert
        mockFactory.Verify(f => f.Create(ReportFormat.Csv), Times.Once);
        mockGenerator.Verify(g => g.GenerateAsync(It.IsAny<ReportParameters>()), Times.Once);
    }
}
