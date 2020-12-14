using Microsoft.Extensions.DependencyInjection;

namespace DistributedCacheDemo.Infra.IoC.Extensions
{
    public static class CacheExtension
    {
        public static IServiceCollection RegisterDistributedCache(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {

                options.Configuration = $"127.0.0.1:6379";
            });

            return services;
        }
    }
}
