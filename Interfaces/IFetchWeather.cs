namespace RealTimeWeather.Interfaces
{
    public interface IFetchWeather
    {

        Task <string> fetchweatherdata(string location);
    }
}
