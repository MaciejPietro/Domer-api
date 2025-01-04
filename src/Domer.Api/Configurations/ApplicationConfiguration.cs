using Domer.Application.Common.Interfaces;
using Domer.Infrastructure.Services;
using System;

namespace Domer.Api.Configurations;

public class ApplicationConfiguration : IApplicationConfiguration
{
    public ApplicationConfiguration()
    {
        EnvService.Load();
    }
    
    public string DbHost => Environment.GetEnvironmentVariable("DB_HOST") ?? throw new InvalidOperationException("DB_HOST not set");
    public string DbDatabase => Environment.GetEnvironmentVariable("DB_DATABASE") ?? throw new InvalidOperationException("DB_DATABASE not set");
    public string DbUsername => Environment.GetEnvironmentVariable("DB_USERNAME") ?? throw new InvalidOperationException("DB_USERNAME not set");
    public string DbPassword => Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD not set");
}