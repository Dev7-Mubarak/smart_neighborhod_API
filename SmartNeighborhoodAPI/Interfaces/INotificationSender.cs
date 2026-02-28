namespace SmartNeighborhoodAPI.Interfaces
{
    public interface INotificationSender
    {
        Task SendAsync(string recipientId, string title, string body);
    }
}
