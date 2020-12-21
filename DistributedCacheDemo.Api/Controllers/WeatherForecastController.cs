using DistributedCacheDemo.Api.Application.Interfaces;
using DistributedCacheDemo.Api.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        public async Task<WeatherForecastDto> Get(string region)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var weatherForecast = await _weatherForecast.GetForecast(region);

            stopwatch.Stop();

            return new WeatherForecastDto()
            {
                WeatherForecast = weatherForecast,
                ElapsedTime = stopwatch.ElapsedMilliseconds
            };
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
