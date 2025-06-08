using Microsoft.AspNetCore.Mvc;
using BookStoreApp.API.Services;

namespace BookStoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            _logger.LogInformation("Getting weather forecast");
            try
            {
                return await _weatherService.GetForecastsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatal Error Occurred.");
                throw;
            }
        }
    }
}
