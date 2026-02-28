using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface INotificationFactory
    {
        INotificationSender Create(NotificationChannel channel);
    }
}
