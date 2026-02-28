using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Services.IssueStatusHandlers;

namespace SmartNeighborhoodAPI.Tests.Services.IssueStatusHandlers;

public class IssueStatusHandlerFactoryTests : IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IServiceProvider> _mockServiceProvider;
    private readonly IssueStatusHandlerFactory _factory;

    public IssueStatusHandlerFactoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _mockMapper = new Mock<IMapper>();
        _mockServiceProvider = new Mock<IServiceProvider>();
        _factory = new IssueStatusHandlerFactory(_mockServiceProvider.Object);
    }

    private void RegisterHandler<THandler>(THandler instance) where THandler : class
    {
        _mockServiceProvider
            .Setup(sp => sp.GetService(typeof(THandler)))
            .Returns(instance);
    }

    [Fact]
    public void Create_WithResolvedStatus_ReturnsResolvedIssueHandler()
    {
        // Arrange
        var handler = new ResolvedIssueHandler(_dbContext, _mockMapper.Object, Mock.Of<ILogger<ResolvedIssueHandler>>());
        RegisterHandler(handler);

        // Act
        var result = _factory.Create(IssueStatus.Resolved);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ResolvedIssueHandler>(result);
    }

    [Fact]
    public void Create_WithOpenStatus_ReturnsOpenIssueHandler()
    {
        // Arrange
        var handler = new OpenIssueHandler(_dbContext, _mockMapper.Object, Mock.Of<ILogger<OpenIssueHandler>>());
        RegisterHandler(handler);

        // Act
        var result = _factory.Create(IssueStatus.Open);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OpenIssueHandler>(result);
    }

    [Fact]
    public void Create_WithInProgressStatus_ReturnsInProgressIssueHandler()
    {
        // Arrange
        var handler = new InProgressIssueHandler(_dbContext, _mockMapper.Object, Mock.Of<ILogger<InProgressIssueHandler>>());
        RegisterHandler(handler);

        // Act
        var result = _factory.Create(IssueStatus.InProgress);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<InProgressIssueHandler>(result);
    }

    [Fact]
    public void Create_WithClosedStatus_ReturnsClosedIssueHandler()
    {
        // Arrange
        var handler = new ClosedIssueHandler(_dbContext, _mockMapper.Object, Mock.Of<ILogger<ClosedIssueHandler>>());
        RegisterHandler(handler);

        // Act
        var result = _factory.Create(IssueStatus.Closed);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ClosedIssueHandler>(result);
    }

    [Fact]
    public void Create_WithUnsupportedStatus_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var invalidStatus = (IssueStatus)999;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _factory.Create(invalidStatus));
        Assert.Equal("status", ex.ParamName);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
