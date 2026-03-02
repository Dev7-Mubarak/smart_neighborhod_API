using Microsoft.Extensions.Logging;
using Moq;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Services.Factories;
using SmartNeighborhoodAPI.Services.ReportGenerators;

namespace SmartNeighborhoodAPI.Tests.Services.Factories;

public class ReportGeneratorFactoryTests
{
    private readonly Mock<IServiceProvider> _mockServiceProvider;
    private readonly ReportGeneratorFactory _factory;

    public ReportGeneratorFactoryTests()
    {
        _mockServiceProvider = new Mock<IServiceProvider>();
        _factory = new ReportGeneratorFactory(_mockServiceProvider.Object);
    }

    private void RegisterGenerator<TGenerator>(TGenerator instance) where TGenerator : class
    {
        _mockServiceProvider
            .Setup(sp => sp.GetService(typeof(TGenerator)))
            .Returns(instance);
    }

    [Fact]
    public void Create_WithPdfFormat_ReturnsPdfReportGenerator()
    {
        // Arrange
        var generator = new PdfReportGenerator(Mock.Of<ILogger<PdfReportGenerator>>());
        RegisterGenerator(generator);

        // Act
        var result = _factory.Create(ReportFormat.Pdf);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PdfReportGenerator>(result);
    }

    [Fact]
    public void Create_WithExcelFormat_ReturnsExcelReportGenerator()
    {
        // Arrange
        var generator = new ExcelReportGenerator(Mock.Of<ILogger<ExcelReportGenerator>>());
        RegisterGenerator(generator);

        // Act
        var result = _factory.Create(ReportFormat.Excel);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ExcelReportGenerator>(result);
    }

    [Fact]
    public void Create_WithCsvFormat_ReturnsCsvReportGenerator()
    {
        // Arrange
        var generator = new CsvReportGenerator(Mock.Of<ILogger<CsvReportGenerator>>());
        RegisterGenerator(generator);

        // Act
        var result = _factory.Create(ReportFormat.Csv);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CsvReportGenerator>(result);
    }

    [Fact]
    public void Create_WithUnsupportedFormat_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var invalidFormat = (ReportFormat)999;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _factory.Create(invalidFormat));
        Assert.Equal("format", ex.ParamName);
    }
}
