using Kompass.Application;
using Kompass.Application.Common.Interfaces;
using Kompass.Domain.Interfaces;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kompass.Infrastructure.Configurations;

public static class DbSetup
{
    public static IServiceCollection AddDbSetup(this IServiceCollection services)
    {
        services.AddSingleton<IApplicationConfiguration, ApplicationConfiguration>();

        services.AddScoped<ISession, Session>();
        services.AddHostedService<ApplicationDbInitializer>();
        services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                var appConfig = services.BuildServiceProvider().GetRequiredService<IApplicationConfiguration>();
                ApplicationDbContextConfiguration.Configure(options, appConfig);
            }


        );

        return services;
    }
}
