using DistributedCacheDemo.Api.Application.Interfaces;
using DistributedCacheDemo.Api.Application.Services;
using DistributedCacheDemo.Api.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DistributedCacheDemo.Api.Infra.IoC.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.TryAddTransient<ITmdbApiCall, TmdbApiCall>();
            services.TryAddSingleton<IWeatherForecast, WeatherForecast>();
            services.TryAddTransient<ISummary, Summary>();
            services.TryAddTransient<IRegion, Region>();
            services.TryAddTransient(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
