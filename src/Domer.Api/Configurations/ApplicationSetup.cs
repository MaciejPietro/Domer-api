using Domer.Application.Common;
using Domer.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Domer.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        services.AddScoped<IContext, ApplicationDbContext>();
        return services;
    }
    
    
}