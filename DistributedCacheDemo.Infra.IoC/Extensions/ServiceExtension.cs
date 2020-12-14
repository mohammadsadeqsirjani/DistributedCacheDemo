using DistributedCacheDemo.Application;
using DistributedCacheDemo.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using IWeatherForecast = DistributedCacheDemo.Application.IWeatherForecast;

namespace DistributedCacheDemo.Infra.IoC.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.TryAddTransient<ITmdbApiCall, TmdbApiCall>();
            services.TryAddTransient<IWeatherForecast, WeatherForecast>();
            services.TryAddTransient<ISummary, Summary>();
            services.TryAddTransient<IRegion, Region>();

            return services;
        }
    }
}
