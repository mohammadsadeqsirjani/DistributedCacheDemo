using DistributedCacheDemo.Api.Application.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Api.Application.Services
{
    public class WeatherForecast : IWeatherForecast
    {
        private readonly ISummary _summary;
        private readonly IRegion _region;
        private readonly IRepository<Domain.Models.Weather.WeatherForecast> _repository;

        public WeatherForecast(ISummary summary, IRegion region, IRepository<Domain.Models.Weather.WeatherForecast> repository)
        {
            _summary = summary;
            _region = region;
            _repository = repository;
        }

        public async Task<Domain.Models.Weather.WeatherForecast> GetForecast(string region)
        {
            var cacheKey = region.ToLower();

            var encodedForecasts = await _repository.TryGetAsync(cacheKey);

            if (encodedForecasts.IsNotNull()) return encodedForecasts;

            encodedForecasts = await GenerateForecast(region);

            await AddForecast(region);

            return encodedForecasts;
        }

        public async Task<Domain.Models.Weather.WeatherForecast> AddForecast(string region)
        {
            var model = await GenerateForecast(region);

            await _repository.TrySetAsync(region, model);

            return model;
        }

        public async Task<Domain.Models.Weather.WeatherForecast> ModifyForecast(string key)
        {
            var model = await GenerateForecast(key);

            await Task.Delay(5000);

            await _repository.TryModifyAsync(key, model);

            return model;
        }

        public Task DeleteForecast(string key)
        {
            _repository.TryDeleteAsync(key);

            return Task.CompletedTask;
        }

        private Task<Domain.Models.Weather.WeatherForecast> GenerateForecast(string region)
        {
            region = region.ToTitleCase();

            if (region.NotIn(_region.GetRegions().ToArray()))
            {
                _region.SetRegion(region);
            }

            var rng = new Random();
            var weatherForecast = Enumerable.Range(1, 10)
                .Select(index => new Domain.Models.Weather.WeatherForecast
                {
                    Region = region,
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = _summary.GetSummary()
                })
                .FirstOrDefault();

            return Task.FromResult(weatherForecast);
        }
    }
}
