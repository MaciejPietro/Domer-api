using Domer.Domain.Interfaces;
using Domer.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Domer.Api.Configurations;

public static class S3StorageSetup
{
    public static IServiceCollection AddS3StorageSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IS3StorageService, AmazonS3StorageService>();

        return services;
    }
}