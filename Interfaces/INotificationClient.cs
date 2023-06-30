namespace RealTimeWeather.Interfaces
{
    public interface INotificationClient
    {
            Task ReceiveNotification(string content);
    }
}
