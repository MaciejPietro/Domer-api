using Domer.Domain.Entities.Auth;
using Domer.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Domer.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    : ControllerBase
{
    private readonly SignInManager<ApplicationUser> signInManager = signInManager;
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(Register model)
    {
        // Check if the username already exists
        if (!string.IsNullOrWhiteSpace(model.Username) && await userManager.FindByNameAsync(model.Username) != null)
        {
            return BadRequest(new { Message = "Username already exists." });
        }
        
        // Check if the email already exists
        if (await userManager.FindByEmailAsync(model.Email) != null)
        {
            return BadRequest(new { Message = "Email already exists." });
        }
        
        ApplicationUser user = new ApplicationUser { UserName = model.Username, Email = model.Email };
    
        IdentityResult result = await userManager.CreateAsync(user, model.Password!);
        
        if (result.Succeeded)
        {
            return Created();
        }
    
        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Login model)
    {
        SignInResult signInResult = await signInManager.PasswordSignInAsync(
            model.Email, 
            model.Password, 
            isPersistent: false, 
            lockoutOnFailure: false
            );

        if (signInResult.Succeeded)
        {
            return Ok();
        }
    
        return BadRequest();
    }

}

