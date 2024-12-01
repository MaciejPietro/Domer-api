using Domer.Domain.Entities.Auth;
using Domer.Infrastructure;
using Domer.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Domer.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender _emailSender)
    : ControllerBase
{

        // private readonly SignInManager<ApplicationUser> signInManager = signInManager;

    [HttpPost("register")]
    public async Task<IActionResult> Register(Register model)
    {
        // Check if the email already exists
        if (await userManager.FindByEmailAsync(model.Email) != null)
        {
            return BadRequest(new { Message = "Email already exists." });
        }
        
        ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
    
        IdentityResult result = await userManager.CreateAsync(user, model.Password);
        
        if (!result.Succeeded)
        {
            return BadRequest();
        }
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var param = new Dictionary<string, string?> { { "token", token }, { " email", user.Email } };
        
        var callbackLink = QueryHelpers.AddQueryString(model.ClientUri!,  param);
        
        
        var message = new Message([user.Email], "Email Confirmation Token", callbackLink);
      
        
        await _emailSender.SendEmailAsync(message);
        
        return Created();
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
    
        return BadRequest(signInResult);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync().ConfigureAwait(false);
        return Ok();
    }
}

