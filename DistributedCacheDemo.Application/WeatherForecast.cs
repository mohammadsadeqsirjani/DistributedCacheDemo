using DistributedCacheDemo.Infra.Data;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Application
{
    public class WeatherForecast : IWeatherForecast
    {
        private readonly ISummary _summary;
        private readonly IRegion _region;
        private readonly IDistributedCache _distributedCache;

        public WeatherForecast(ISummary summary, IRegion region, IDistributedCache distributedCache)
        {
            _summary = summary;
            _region = region;
            _distributedCache = distributedCache;
        }

        public async Task<List<Domain.Models.Weather.WeatherForecast>> GetWeatherForecast(string region)
        {
            var cacheKey = region.ToLower();

            List<Domain.Models.Weather.WeatherForecast> forecastList;
            string serializedForecasts;

            var encodedForecasts = await _distributedCache.GetAsync(cacheKey);

            if (encodedForecasts != null)
            {
                serializedForecasts = Encoding.UTF8.GetString(encodedForecasts);
                forecastList = JsonConvert.DeserializeObject<List<Domain.Models.Weather.WeatherForecast>>(serializedForecasts)
                    .Where(w => w.Region == region)
                    .ToList();
            }
            else
            {
                forecastList = (await GetWeatherForecastList())
                    .Where(w => string.Equals(w.Region, region, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
                serializedForecasts = JsonConvert.SerializeObject(forecastList);
                encodedForecasts = Encoding.UTF8.GetBytes(serializedForecasts);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5));
                await _distributedCache.SetAsync(cacheKey, encodedForecasts, options);
            }
            return forecastList.Where(w => w.Region == region).ToList();
        }

        private Task<List<Domain.Models.Weather.WeatherForecast>> GetWeatherForecastList()
        {
            var rng = new Random();
            var weatherForecasts = Enumerable.Range(1, 10)
                .Select(index => new Domain.Models.Weather.WeatherForecast
                {
                    Region = _region.GetRegion(),
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = _summary.GetSummary()
                })
                .ToList();

            return Task.FromResult(weatherForecasts);
        }

        private static HttpClient GetHttpClient(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
