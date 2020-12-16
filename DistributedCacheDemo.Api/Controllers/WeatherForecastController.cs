using DistributedCacheDemo.Application;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistributedCacheDemo.Infra.Data;
using WeatherForecast = DistributedCacheDemo.Domain.Models.Weather.WeatherForecast;

namespace DistributedCacheDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecast _weatherForecast;

        public WeatherForecastController(IWeatherForecast weatherForecast)
        {
            _weatherForecast = weatherForecast;
        }

        [HttpGet("[action]/{region}")]
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(string region)
        {
            var weatherForecasts = await _weatherForecast.GetWeatherForecast(region);

            return weatherForecasts;
        }
    }
}
