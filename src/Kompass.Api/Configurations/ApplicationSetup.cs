using Kompass.Application.Common;
using Kompass.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Kompass.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        services.AddScoped<IContext, ApplicationDbContext>();
        return services;
    }
    
    
}