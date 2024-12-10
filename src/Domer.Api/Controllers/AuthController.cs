using AutoMapper;
using Domer.Application.DTOs.Queries;
using Domer.Domain.Common.Interfaces;
using Domer.Domain.Entities.Auth;
using Domer.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Domer.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService  emailService, IMapper mapper)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(Register model)
    {
        if (await userManager.FindByEmailAsync(model.Email) != null)
        {
            return BadRequest(new { Message = "Email already exists." });
        }
        
        ApplicationUser user = new() { UserName = model.Email, Email = model.Email };
    
        try
        {
            IdentityResult result = await userManager.CreateAsync(user, model.Password);
        
            if (!result.Succeeded)
            {
                return BadRequest();
            }
        
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            
            string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));


            Dictionary<string, string?> param = new()
            { 
                { "token", encodedToken }, 
                { "email", user.Email } 
            };
        
            string callbackLink = QueryHelpers.AddQueryString(model.ClientUri!,  param);
        
            await emailService.SendRegistrationConfirmationEmailAsync(user.Email, callbackLink);
      
            return Created();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("emailconfirmation")]
    public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(email);
        

        if (user == null)
        {
            return BadRequest("Invalid Email Confirmation Request");
        }

        string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
    
        IdentityResult confirmResult = await userManager.ConfirmEmailAsync(user, decodedToken);
        
        Console.WriteLine(confirmResult);


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

    
    [HttpPost("remindpassword")]
    public async Task<IActionResult> SendResetPasswordLink(ResetPasswordLink model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ApplicationUser? user = await userManager.FindByEmailAsync(model.Email);
    
        if (user == null)
        {
            return BadRequest("Użytkownik o podanym adresie email nie istnieje.");
        }

        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            return BadRequest("Użytkownik o podanym adresie nie ma potwierdzonego adresu email.");
        }

        string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

        string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));

        Dictionary<string, string?> param = new()
        { 
            { "token", encodedToken }, 
            { "email", user.Email } 
        };
    
        string resetLink = QueryHelpers.AddQueryString(model.ClientUri!, param);

        try 
        {
            await emailService.SendResetPasswordEmailAsync(user.Email!, resetLink);
            return Ok($"Email z linkiem aktywacyjnym został wysłany na adres {user.Email}.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error sending password reset email: {ex.Message}");
            return StatusCode(500, "Coś poszło nie tak.");
        }
    }
    
    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword(ResetPassword model)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(model.Email!);

        if (user == null) return BadRequest();
        
        string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
        
        IdentityResult resetPassResult = await userManager.ResetPasswordAsync(user, decodedToken, model.Password);
        
        if (!resetPassResult.Succeeded)
        {
            return resetPassResult.Errors.Any(e => 
                e.Code.Contains("InvalidToken", StringComparison.OrdinalIgnoreCase)) ? BadRequest("Niepoprawny token.") : BadRequest(resetPassResult.Errors);
        }
        
        return Ok("Hasło zostało zmienione");
    }


    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(Login model)
    {
        try
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(model.Email!);

            if (user == null) return BadRequest("Nie znaleźliśmy użytkownika o takim adresie email.");  
            
            
            
            SignInResult signInResult = await signInManager.PasswordSignInAsync(
                model.Email, 
                model.Password, 
                isPersistent: false, 
                lockoutOnFailure: false
            );


            if (!signInResult.Succeeded)
            {
                return BadRequest("Błędny email lub hasło");
            }

                
            if (user == null)
                return Unauthorized();
            
            UserDto? responseDto = mapper.Map<UserDto>(user);
                
            return Ok(responseDto);

        }
        catch (Exception ex)
        {
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

