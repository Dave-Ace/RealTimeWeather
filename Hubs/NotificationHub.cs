using Microsoft.AspNetCore.SignalR;
using RealTimeWeather.Interfaces;

namespace RealTimeWeather.Hubs
{
    public sealed class NotificationHub : Hub<INotificationClient>
    {
        private readonly IConfiguration _configuration;

        public NotificationHub(IConfiguration configuration)
            {
                _configuration = configuration;
            }
        public async Task SendNotification(string location)
        {
            var weatherData = await fetchweatherdata(location);

            await Clients.All.ReceiveNotification(weatherData);
        }

        public async Task<string> fetchweatherdata(string location)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    HttpResponseMessage data = await client.GetAsync($"https://api.tomorrow.io/v4/weather/realtime?location={location}&apikey={_configuration["WeatherApi"]}");

                    if (data.IsSuccessStatusCode)
                    {
                        var dataBody = await data.Content.ReadAsStringAsync();

                        return dataBody;
                    }
                    else
                    {
                        return data.ReasonPhrase;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}