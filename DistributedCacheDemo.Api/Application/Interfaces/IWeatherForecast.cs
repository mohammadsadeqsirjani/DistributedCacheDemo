using DistributedCacheDemo.Api.Domain.Models.Weather;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Api.Application.Interfaces
{
    public interface IWeatherForecast
    {
        Task<WeatherForecast> GetForecast(string key);
        Task<WeatherForecast> AddForecast(string region);
        Task<WeatherForecast> ModifyForecast(string key);
        Task DeleteForecast(string key);
    }
}