
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domer.Api.Configurations;

public static class CorsSetup
{
    public static IServiceCollection AddCorsSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("LocalhostPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

        });

        return services;
    }
    
    public static IApplicationBuilder UseCorsSetup(this IApplicationBuilder app)
    {
        app.UseCors("LocalhostPolicy");

        return app;
    }
}