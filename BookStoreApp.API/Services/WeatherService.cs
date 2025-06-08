using BookStoreApp.API;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BookStoreApp.API.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<WeatherService> _logger;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private const string CacheKey = "weather_forecasts";

        public WeatherService(IMemoryCache cache, ILogger<WeatherService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public Task<IEnumerable<WeatherForecast>> GetCachedForecastsAsync()
        {
            if (!_cache.TryGetValue(CacheKey, out IEnumerable<WeatherForecast>? forecasts))
            {
                _logger.LogInformation("Generating new weather forecasts");
                forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    })
                    .ToArray();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(CacheKey, forecasts, cacheOptions);
            }
            else
            {
                _logger.LogInformation("Retrieving weather forecasts from cache");
            }

            return Task.FromResult(forecasts!);
        }
    }
}
