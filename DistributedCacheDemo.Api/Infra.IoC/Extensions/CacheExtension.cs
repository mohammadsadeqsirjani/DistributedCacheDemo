using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace DistributedCacheDemo.Api.Infra.IoC.Extensions
{
    public static class CacheExtension
    {
        public static IServiceCollection RegisterDistributedCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                var cacheConfiguration = configuration.GetSection("DistributedCache").Get<DistributedCacheConfiguration>();

                options.Configuration = $"{cacheConfiguration.Host}:{cacheConfiguration.Port}";
            });

            var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);

            return services;
        }
    }

    public class DistributedCacheConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
    }
}
