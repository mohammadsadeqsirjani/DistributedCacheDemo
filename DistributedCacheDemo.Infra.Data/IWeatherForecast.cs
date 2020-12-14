using DistributedCacheDemo.Domain.Models.Weather;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Infra.Data
{
    public interface IWeatherForecast
    {
        Task<List<WeatherForecast>> GetWeatherForecastList();
    }
}