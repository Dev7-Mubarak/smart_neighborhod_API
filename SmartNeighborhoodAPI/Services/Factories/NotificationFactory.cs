using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Services.Notifications;

namespace SmartNeighborhoodAPI.Services.Factories
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INotificationSender Create(NotificationChannel channel) => channel switch
        {
            NotificationChannel.Push => _serviceProvider.GetRequiredService<PushNotificationSender>(),
            NotificationChannel.Email => _serviceProvider.GetRequiredService<EmailNotificationSender>(),
            NotificationChannel.InApp => _serviceProvider.GetRequiredService<InAppNotificationSender>(),
            _ => throw new ArgumentOutOfRangeException(nameof(channel), channel,
                     $"No notification sender is registered for channel '{channel}'.")
        };
    }
}
