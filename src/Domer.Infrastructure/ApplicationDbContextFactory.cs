using Domer.Application.Common.Interfaces;
using Domer.Infrastructure.Configurations;
using Domer.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Domer.Infrastructure;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        EnvService.Load();
        
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Manually load environment variables
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? throw new InvalidOperationException("DB_HOST not set");
        var dbDatabase = Environment.GetEnvironmentVariable("DB_DATABASE") ?? throw new InvalidOperationException("DB_DATABASE not set");
        var dbUsername = Environment.GetEnvironmentVariable("DB_USERNAME") ?? throw new InvalidOperationException("DB_USERNAME not set");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD not set");

        // Create a temporary configuration object
        var config = new TempAppConfiguration(dbHost, dbDatabase, dbUsername, dbPassword);

        // Reuse the centralized configuration logic
        ApplicationDbContextConfiguration.Configure(optionsBuilder, config);

        return new ApplicationDbContext(optionsBuilder.Options);
    }

    // Temporary implementation of IAppConfiguration for design-time
    private class TempAppConfiguration : IApplicationConfiguration
    {
        public TempAppConfiguration(string dbHost, string dbDatabase, string dbUsername, string dbPassword)
        {
            DbHost = dbHost;
            DbDatabase = dbDatabase;
            DbUsername = dbUsername;
            DbPassword = dbPassword;
        }

        public string DbHost { get; }
        public string DbDatabase { get; }
        public string DbUsername { get; }
        public string DbPassword { get; }
    }
}