using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.Notifications
{
    public class PushNotificationSender : INotificationSender
    {
        private readonly ILogger<PushNotificationSender> _logger;

        public PushNotificationSender(ILogger<PushNotificationSender> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string recipientId, string title, string body)
        {
            _logger.LogInformation("Sending Push Notification to {recipientId}... Title: {title}, Body: {body}",
                recipientId, title, body);
            return Task.CompletedTask;
        }
    }
}
