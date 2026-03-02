using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Services.IssueStatusHandlers;

namespace SmartNeighborhoodAPI.Tests.Services.IssueStatusHandlers;

public class ResolvedIssueHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<ResolvedIssueHandler>> _mockLogger;
    private readonly Mock<INotificationFactory> _mockNotificationFactory;
    private readonly Mock<INotificationSender> _mockNotificationSender;
    private readonly ResolvedIssueHandler _handler;

    public ResolvedIssueHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<ResolvedIssueHandler>>();
        _mockNotificationSender = new Mock<INotificationSender>();
        _mockNotificationFactory = new Mock<INotificationFactory>();

        _mockNotificationFactory
            .Setup(f => f.Create(NotificationChannel.Push))
            .Returns(_mockNotificationSender.Object);

        _mockNotificationSender
            .Setup(s => s.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        _handler = new ResolvedIssueHandler(
            _dbContext,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockNotificationFactory.Object);
    }

    [Fact]
    public async Task HandleAsync_WithValidIssue_SetsResolvedAtAndSendsPushNotification()
    {
        // Arrange
        const string reporterId = "user-1";
        var issue = new Issue
        {
            Id = Guid.NewGuid(),
            Title = "Test Issue",
            Description = "Test Description",
            Category = "Test",
            Priority = IssuePriority.High,
            Status = IssueStatus.Resolved,
            ReporterId = reporterId,
            Attachments = []
        };

        var dto = new UpdateIssueDto { Status = "Resolved" };
        var before = DateTime.UtcNow;

        // Act
        var result = await _handler.HandleAsync(issue, dto);
        var after = DateTime.UtcNow;

        // Assert — resolved timestamp
        Assert.True(result.IsSuccess);
        Assert.True(result.Data);
        Assert.NotNull(issue.ResolvedAt);
        Assert.InRange(issue.ResolvedAt!.Value, before, after);

        // Assert — Push notification sent exactly once to the reporter
        _mockNotificationFactory.Verify(
            f => f.Create(NotificationChannel.Push), Times.Once);

        _mockNotificationSender.Verify(
            s => s.SendAsync(
                reporterId,
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
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

        // Two calls => two notifications
        _mockNotificationSender.Verify(
            s => s.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
            Times.Exactly(2));
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
