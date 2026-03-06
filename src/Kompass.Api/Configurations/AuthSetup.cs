using Kompass.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kompass.Api.Configurations;

public static class AuthSetup
{
    public static IServiceCollection AddAuthSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme, options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.Cookie.HttpOnly = true;
            // TODO enable SameSite mode before release
            options.Cookie.SameSite = SameSiteMode.None;
            options.SlidingExpiration = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            
        });

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        return services;
    }
    
    public static IApplicationBuilder UseAuthSetup(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}