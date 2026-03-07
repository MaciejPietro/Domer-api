using Kompass.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kompass.Api.Configurations;

public class CustomUserClaimsPrincipalFactory(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor)
    : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>(userManager, roleManager, optionsAccessor)
{
    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);

        // Add roles as claims
        var roles = await UserManager.GetRolesAsync(user);
        var claimsIdentity = principal.Identity as ClaimsIdentity;

        foreach (var role in roles)
        {
            claimsIdentity?.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return principal;
    }
}