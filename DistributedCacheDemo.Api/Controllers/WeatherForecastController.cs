using DistributedCacheDemo.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherForecast = DistributedCacheDemo.Api.Domain.Models.Weather.WeatherForecast;

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
        public async Task<WeatherForecast> Get(string region)
        {
            var weatherForecast = await _weatherForecast.GetForecast(region);

            return weatherForecast;
        }

        [HttpPost("[action]/{region}")]
        public async Task<WeatherForecast> Post(string region)
        {
            var model = await _weatherForecast.AddForecast(region);

            return model;
        }

        [HttpPut("[action]/{region}")]
        public async Task<WeatherForecast> Put(string region)
        {
            var model = await _weatherForecast.ModifyForecast(region);

            return model;
        }

        [HttpDelete("[action]/{region}")]
        public async Task Delete(string region)
        {
            await _weatherForecast.DeleteForecast(region);
        }
    }
}
