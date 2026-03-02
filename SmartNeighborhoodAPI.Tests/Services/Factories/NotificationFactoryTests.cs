using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Services.Factories;
using SmartNeighborhoodAPI.Services.Notifications;

namespace SmartNeighborhoodAPI.Tests.Services.Factories;

public class NotificationFactoryTests
{
    private readonly Mock<IServiceProvider> _mockServiceProvider;
    private readonly NotificationFactory _factory;

    public NotificationFactoryTests()
    {
        _mockServiceProvider = new Mock<IServiceProvider>();
        _factory = new NotificationFactory(_mockServiceProvider.Object);
    }

    /// <summary>
    /// Helper that registers a concrete sender instance so that
    /// GetRequiredService&lt;T&gt;() (which resolves via GetService) returns it.
    /// </summary>
    private void RegisterSender<TSender>(TSender instance) where TSender : class
    {
        _mockServiceProvider
            .Setup(sp => sp.GetService(typeof(TSender)))
            .Returns(instance);
    }

    [Fact]
    public void Create_WithPushChannel_ReturnsPushNotificationSender()
    {
        // Arrange
        var sender = new PushNotificationSender(Mock.Of<ILogger<PushNotificationSender>>());
        RegisterSender(sender);

        // Act
        var result = _factory.Create(NotificationChannel.Push);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PushNotificationSender>(result);
    }

    [Fact]
    public void Create_WithEmailChannel_ReturnsEmailNotificationSender()
    {
        // Arrange
        var sender = new EmailNotificationSender(Mock.Of<ILogger<EmailNotificationSender>>());
        RegisterSender(sender);

        // Act
        var result = _factory.Create(NotificationChannel.Email);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<EmailNotificationSender>(result);
    }

    [Fact]
    public void Create_WithInAppChannel_ReturnsInAppNotificationSender()
    {
        // Arrange
        var sender = new InAppNotificationSender(Mock.Of<ILogger<InAppNotificationSender>>());
        RegisterSender(sender);

        // Act
        var result = _factory.Create(NotificationChannel.InApp);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<InAppNotificationSender>(result);
    }

    [Fact]
    public void Create_WithUnsupportedChannel_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var invalidChannel = (NotificationChannel)999;

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => _factory.Create(invalidChannel));

        Assert.Equal("channel", ex.ParamName);
    }
}
