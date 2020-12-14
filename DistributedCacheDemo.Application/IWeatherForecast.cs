using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Application
{
    public interface IWeatherForecast
    {
        Task<List<Domain.Models.Weather.WeatherForecast>> GetWeatherForecast(string region);
    }
}