using DistributedCacheDemo.Api.Domain.Models.Weather;

namespace DistributedCacheDemo.Api.Application.ViewModels
{
    public class WeatherForecastDto
    {
        public WeatherForecast WeatherForecast { get; set; }
        public long ElapsedTime { get; set; }
    }
}
