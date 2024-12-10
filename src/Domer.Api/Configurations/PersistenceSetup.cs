using Domer.Application;
using Domer.Domain.Interfaces;
using Domer.Infrastructure;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Domer.Api.Configurations;

public static class DbSetup
{
    public static IServiceCollection AddDbSetup(this IServiceCollection services, IConfiguration configuration)
    {



        string? DbHost = Environment.GetEnvironmentVariable("DB_HOST");
        string? DbDatabase = Environment.GetEnvironmentVariable("DB_DATABASE");
        string? DbUsername = Environment.GetEnvironmentVariable("DB_USERNAME");
        string? DbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

        services.AddScoped<ISession, Session>();
        services.AddHostedService<ApplicationDbInitializer>();
        services.AddDbContextPool<ApplicationDbContext>(o =>
        {
            o.UseNpgsql($"Host={DbHost};Port=5432;Database={DbDatabase};Username={DbUsername};Password={DbPassword}");
            o.UseExceptionProcessor();
        });

        return services;
    }
}