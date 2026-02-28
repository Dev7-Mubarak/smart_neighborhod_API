using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.Notifications
{
    public class InAppNotificationSender : INotificationSender
    {
        private readonly ILogger<InAppNotificationSender> _logger;

        public InAppNotificationSender(ILogger<InAppNotificationSender> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string recipientId, string title, string body)
        {
            _logger.LogInformation("Sending In-App Notification to {recipientId}... Title: {title}, Body: {body}",
                recipientId, title, body);
            return Task.CompletedTask;
        }
    }
}
