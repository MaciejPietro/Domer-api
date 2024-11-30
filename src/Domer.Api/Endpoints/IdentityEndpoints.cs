using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace Domer.Api.Endpoints;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapCustomIdentityApi<TUser>(this IEndpointRouteBuilder endpoints) 
        where TUser : class, new()
    {
        // Map other default identity endpoints
        endpoints.MapIdentityApi<TUser>();
        
        var routeGroup = endpoints.MapGroup("");

        // Custom register endpoint
        routeGroup.MapPost("/register", async (
            [FromBody] RegisterRequest registration,
            [FromServices] UserManager<TUser> userManager,
            [FromServices] IUserConfirmation<TUser> userConfirmation) =>
        {
            var user = new TUser();
        
            // Use reflection or a mapping method to set user properties
            var propertyInfo = typeof(TUser).GetProperty("UserName");
            propertyInfo?.SetValue(user, registration.Email);
        
            var result = await userManager.CreateAsync(user, registration.Password);

            if (result.Succeeded)
            {
                // Return 201 Created without setting HttpOnly cookie
                return Results.Created($"/identity/users/{user}", new { UserId = user });
            }

            // Handle errors
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Results.BadRequest(new { Errors = errors });
        }).AllowAnonymous();

        return endpoints;
    }

    // Supporting request model
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}