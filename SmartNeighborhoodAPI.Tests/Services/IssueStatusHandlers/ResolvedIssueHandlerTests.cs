using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Services.IssueStatusHandlers;

namespace SmartNeighborhoodAPI.Tests.Services.IssueStatusHandlers;

public class ResolvedIssueHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<ResolvedIssueHandler>> _mockLogger;
    private readonly ResolvedIssueHandler _handler;

    public ResolvedIssueHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<ResolvedIssueHandler>>();

        _handler = new ResolvedIssueHandler(_dbContext, _mockMapper.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task HandleAsync_WithValidIssue_SetsResolvedAtToUtcNow()
    {
        // Arrange
        var issue = new Issue
        {
            Id = Guid.NewGuid(),
            Title = "Test Issue",
            Description = "Test Description",
            Category = "Test",
            Priority = IssuePriority.High,
            Status = IssueStatus.Resolved,
            ReporterId = "user-1",
            Attachments = []
        };

        var dto = new UpdateIssueDto { Status = "Resolved" };
        var before = DateTime.UtcNow;

        // Act
        var result = await _handler.HandleAsync(issue, dto);

        var after = DateTime.UtcNow;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Data);
        Assert.NotNull(issue.ResolvedAt);
        Assert.InRange(issue.ResolvedAt!.Value, before, after);
    }

    [Fact]
    public async Task HandleAsync_WithValidIssue_ReturnsSuccessfulApiResponse()
    {
        // Arrange
        var issue = new Issue
        {
            Id = Guid.NewGuid(),
            Title = "Test Issue",
            Description = "Test Description",
            Category = "Test",
            Priority = IssuePriority.Low,
            Status = IssueStatus.Resolved,
            ReporterId = "user-2",
            Attachments = []
        };

        var dto = new UpdateIssueDto { Status = "Resolved" };

        // Act
        var result = await _handler.HandleAsync(issue, dto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Data);
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task HandleAsync_WhenCalledMultipleTimes_OverwritesResolvedAt()
    {
        // Arrange
        var issue = new Issue
        {
            Id = Guid.NewGuid(),
            Title = "Test Issue",
            Description = "Overwrite test",
            Category = "Test",
            Priority = IssuePriority.Medium,
            Status = IssueStatus.Resolved,
            ReporterId = "user-3",
            Attachments = []
        };

        var dto = new UpdateIssueDto { Status = "Resolved" };

        // Act
        await _handler.HandleAsync(issue, dto);
        var firstResolvedAt = issue.ResolvedAt;

        await Task.Delay(10); // ensure time passes
        await _handler.HandleAsync(issue, dto);
        var secondResolvedAt = issue.ResolvedAt;

        // Assert
        Assert.NotNull(firstResolvedAt);
        Assert.NotNull(secondResolvedAt);
        Assert.True(secondResolvedAt >= firstResolvedAt);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
