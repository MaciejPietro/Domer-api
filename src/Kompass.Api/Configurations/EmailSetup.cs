
using Kompass.Application.Common.Services;
using Kompass.Domain.Common.Interfaces;
using Kompass.Infrastructure.Email;
using Kompass.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kompass.Api.Configurations;

public static class EmailSetup
{
    public static IServiceCollection AddEmailSetup(this IServiceCollection services, IConfiguration configuration)
    {
        EnvService.Load();
        
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IEmailService, EmailService>();
        
        var emailConfig = new EmailConfiguration
        {
            From = Environment.GetEnvironmentVariable("EMAIL_FROM"),
            SmtpServer = Environment.GetEnvironmentVariable("EMAIL_SMTP_SERVER"),
            Port = int.TryParse(Environment.GetEnvironmentVariable("EMAIL_SMTP_PORT"), out int port) ? port : 587,
            UserName = Environment.GetEnvironmentVariable("EMAIL_USERNAME"),
            Password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
        };

        services.AddSingleton(emailConfig);

        return services;
    }
}