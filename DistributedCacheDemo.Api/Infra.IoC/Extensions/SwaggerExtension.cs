using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DistributedCacheDemo.Api.Infra.IoC.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DistributedCacheDemo",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IApplicationBuilder EnableSwaggerMiddleware(this IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DistributedCacheDemo v1"));

            return app;
        }
    }
}
