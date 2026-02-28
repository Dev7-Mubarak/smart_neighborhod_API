using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.Notifications
{
    public class EmailNotificationSender : INotificationSender
    {
        private readonly ILogger<EmailNotificationSender> _logger;

        public EmailNotificationSender(ILogger<EmailNotificationSender> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string recipientId, string title, string body)
        {
            _logger.LogInformation("Sending Email Notification to {recipientId}... Title: {title}, Body: {body}",
                recipientId, title, body);
            return Task.CompletedTask;
        }
    }
}
