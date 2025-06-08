using BookStoreApp.API;

namespace BookStoreApp.API.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetForecastsAsync();
    }
}
