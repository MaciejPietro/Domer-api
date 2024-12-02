using Domer.Application.Auth;
using Domer.Domain.Auth.Interfaces;
using Domer.Domain.Interface;
using Domer.Infrastructure;
using Domer.Infrastructure.Services;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Domer.Api.Configurations;

public static class PersistenceSetup
{
    public static IServiceCollection AddPersistenceSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailSender, EmailSender>();

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