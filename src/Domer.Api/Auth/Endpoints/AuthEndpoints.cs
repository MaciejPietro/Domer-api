using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Domer.Api.Auth.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapCustomIdentityApi<TUser>(this IEndpointRouteBuilder endpoints) 
        where TUser : class, new()
    {
        endpoints.MapIdentityApi<TUser>();
        
        RouteGroupBuilder routeGroup = endpoints.MapGroup("");

        routeGroup.MapPost("/register", async (
            [FromBody] RegisterRequest registration,
            [FromServices] UserManager<TUser> userManager,
            [FromServices] IUserConfirmation<TUser> userConfirmation) =>
        {
            TUser user = new TUser();
        
            PropertyInfo? propertyInfo = typeof(TUser).GetProperty("UserName");
            propertyInfo?.SetValue(user, registration.Email);
        
            IdentityResult result = await userManager.CreateAsync(user, registration.Password);

            if (result.Succeeded)
            {
                return Results.Created($"/identity/users/{user}", new { UserId = user });
            }

            List<string> errors = result.Errors.Select(e => e.Description).ToList();
            return Results.BadRequest(new { Errors = errors });
        }).AllowAnonymous();

        return endpoints;
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}