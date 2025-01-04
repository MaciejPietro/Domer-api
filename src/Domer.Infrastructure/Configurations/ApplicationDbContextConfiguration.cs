using Domer.Application.Common.Interfaces;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System;

namespace Domer.Infrastructure.Configurations;

public abstract class ApplicationDbContextConfiguration
{
    public static void Configure(DbContextOptionsBuilder optionsBuilder, IApplicationConfiguration config)
    {
        
        
        optionsBuilder.UseNpgsql($"Host={config.DbHost};Port=5432;Database={config.DbDatabase};Username={config.DbUsername};Password={config.DbPassword}");
        optionsBuilder.UseExceptionProcessor(); // Add other common configurations here
    }
}