using Kompass.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Kompass.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        return services;
    }
}