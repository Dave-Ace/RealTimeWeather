using RealTimeWeather.Interfaces;
using System;
using System.Net.Http;
namespace RealTimeWeather.Implemenation
{
    public class FetchWeather : IFetchWeather
    {

        private readonly IConfiguration _configuration;
        public FetchWeather(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> fetchweatherdata(string location)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    HttpResponseMessage data = await client.GetAsync($"https://api.tomorrow.io/v4/weather/realtime?location={location}&apikey=uTKPnKPnwQLMDFgVTbKGnuf6UTMxNAb0");

                    if (data.IsSuccessStatusCode)
                    {
                        string dataBody = await data.Content.ReadAsStringAsync();       

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