using Kompass.Domain.Enums.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Infrastructure;

public class ApplicationDbInitializer(ILogger<ApplicationDbInitializer> logger, IServiceProvider serviceProvider)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
        logger.LogInformation("Running migrations for {Context}", nameof(ApplicationDbContext));
        await strategy.ExecuteAsync(async () => await context.Database.MigrateAsync(cancellationToken: cancellationToken));
        await context.Database.MigrateAsync(cancellationToken: cancellationToken);
        logger.LogInformation("Migrations applied successfully");

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        foreach (var role in Enum.GetValues<UserRole>())
        {
            var roleName = role.ToString();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            }
        }
        logger.LogInformation("Default roles seeded successfully");
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;
}