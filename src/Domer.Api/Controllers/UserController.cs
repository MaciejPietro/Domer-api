using AutoMapper;
using Domer.Application.Auth.DTOs;
using Domer.Domain.Auth.Entities;
using Domer.Domain.Common.Interfaces;
using Domer.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Domer.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService  emailService, IMapper mapper)
    : ControllerBase
{

       
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
        
        string token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var param = new Dictionary<string, string?> { 
            { "token", HttpUtility.UrlEncode(token) }, 
            { "email", user.Email } 
        };
        
       
        
        var callbackLink = QueryHelpers.AddQueryString(model.ClientUri!,  param);
        
        
    

        await emailService.SendRegistrationConfirmationEmailAsync(user.Email, callbackLink);
      
        return Created();
    }

    [HttpGet("emailconfirmation")]
    public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return BadRequest("Invalid Email Confirmation Request");
        }


        string decodedToken = HttpUtility.UrlDecode(token);
    
        IdentityResult confirmResult = await userManager.ConfirmEmailAsync(user, decodedToken);

        if (confirmResult.Succeeded)
        {
            return Ok();
        }

        foreach (IdentityError error in confirmResult.Errors)
        {
            Console.WriteLine($"Token Confirmation Error: {error.Description}");
        }
        return BadRequest("Invalid Email Confirmation Request");

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(Login model)
    {
        try
        {
            SignInResult signInResult = await signInManager.PasswordSignInAsync(
                model.Email, 
                model.Password, 
                isPersistent: false, 
                lockoutOnFailure: false
            );

            
            if (signInResult.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                
                if (user == null)
                    return Unauthorized();
            
                var responseDto = mapper.Map<UserDto>(user);
                
                return Ok(responseDto);
            }
            
            return BadRequest(signInResult);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, new { message = ex });
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync().ConfigureAwait(false);
        return Ok();
    }
}

