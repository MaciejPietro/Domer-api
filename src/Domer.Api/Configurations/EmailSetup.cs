
using Domer.Domain.Interface;
using Domer.Infrastructure.Configuration;
using Domer.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Domer.Api.Configurations;

public static class EmailSetup
{
    public static IServiceCollection AddEmailSetup(this IServiceCollection services, IConfiguration configuration)
    {
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