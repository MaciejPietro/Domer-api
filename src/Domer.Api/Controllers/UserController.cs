using AutoMapper;
using Domer.Application.Commands.User.UpdateUser;
using Domer.Application.DTOs;
using Domer.Application.DTOs.Queries;
using Domer.Domain.Common.Interfaces;
using Domer.Domain.Entities.Auth;
using Domer.Domain.Entities.User;
using Domer.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Domer.Api.Controllers;

[Route("api/user")]
[ApiController]
public class UserController(IMediator mediator, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService  emailService, IMapper mapper)
    : ControllerBase
{

    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        ApplicationUser? currentUser = await userManager.GetUserAsync(User);

        if (currentUser == null)
        {
            return Unauthorized(); 
        }

        UserDto userDto = new()
        {
            Id = currentUser.Id,
            Email = currentUser.Email,
            IsEmailConfirmed = currentUser.EmailConfirmed,
        };

        return Ok(userDto);
    }
    
    [HttpPatch]
    [Authorize]
    public async Task<ActionResult> UpdateUser(UpdateUserCommand command)
    {
        return Ok(await mediator.Send(command));
    }


    // public async Task<IActionResult> UpdateUser([FromBody] UpdateUser model)
    // {
    //     // ApplicationUser? user = await userManager.FindByIdAsync(model.Id);
    //     //
    //     // if (user == null)
    //     // {
    //     //     return BadRequest();
    //     // }
    //     //
    //     // if(!string.IsNullOrEmpty(model.Email))
    //     // {
    //     //     if (await userManager.FindByEmailAsync(model.Email) != null)
    //     //         return BadRequest(new { Message = "Email already exists." });
    //     //
    //     //     if (string.IsNullOrEmpty(model.ClientUri))
    //     //         return BadRequest(new { Message = "ClientUri is required to change email." });
    //     //    
    //     //     if (model.Email != user.Email)
    //     //     {
    //     //         IdentityResult setEmailResult = await userManager.SetEmailAsync(user, model.Email);
    //     //         IdentityResult setUserNameResult = await userManager.SetUserNameAsync(user, model.Email);
    //     //
    //     //         if (!setEmailResult.Succeeded || !setUserNameResult.Succeeded)
    //     //         {
    //     //             return BadRequest(new { Message = "Something went wrong." });
    //     //         }
    //     //
    //     //         await ResendEmailConfirmation(new ResendEmailConfirmation 
    //     //         { 
    //     //             Email = model.Email!, 
    //     //             ClientUri = model.ClientUri!
    //     //         });
    //     //     }
    //     // }
    //     //
    //     //
    //     // if (!string.IsNullOrEmpty(model.Password))
    //     // {
    //     //     if (string.IsNullOrEmpty(model.CurrentPassword))
    //     //     {
    //     //         return BadRequest("Current password is required to change the password.");
    //     //     }
    //     //     
    //     //     bool isCurrentPasswordCorrect = await userManager.CheckPasswordAsync(user, model.CurrentPassword);
    //     //
    //     //     if (!isCurrentPasswordCorrect)
    //     //     {
    //     //         return BadRequest("Current password is incorrect.");
    //     //     }
    //     //     
    //     //     string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
    //     //
    //     //     IdentityResult resetPassResult = await userManager.ResetPasswordAsync(user, resetToken, model.Password);
    //     //
    //     //     if (!resetPassResult.Succeeded)
    //     //     {
    //     //         return BadRequest(resetPassResult.Errors);
    //     //     }
    //     // }
    //     //
    //     //  
    //     // await userManager.UpdateAsync(user);
    //     //
    //     // UserDto? responseDto = mapper.Map<UserDto>(user);
    //     //         
    //     // return Ok(responseDto);
    //     
    // }

    [HttpDelete()]
    [Authorize]
    public async Task<IActionResult> DeleteUser([FromBody] Delete model)
    {
        if (string.IsNullOrEmpty(model.Password))
        {
            return BadRequest("Password is required to delete account.");
        }
        
        ApplicationUser? user = await userManager.FindByIdAsync(model.Id);
        
        if (user == null)
        {
            return BadRequest("User not found");
        }
        
        bool isCurrentPasswordCorrect = await userManager.CheckPasswordAsync(user, model.Password);
    
        if (!isCurrentPasswordCorrect)
        {
            return BadRequest("Błędne hasło.");
        }
        
        IdentityResult deleteResult = await userManager.DeleteAsync(user);

        if (!deleteResult.Succeeded)
        {
            return BadRequest(deleteResult.Errors);
        }

        await signInManager.SignOutAsync();

        return Ok(new { Message = "Account successfully deleted." });
    }
    
    [HttpPost("resend-emailconfirmation")]
    public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendEmailConfirmation model)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest(new { Message = "User not found." });
        }

        if (await userManager.IsEmailConfirmedAsync(user))
        {
            return BadRequest(new { Message = "Email is already confirmed." });
        }

        try
        {
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            
            string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            Dictionary<string, string> param = new()
            {
                { "token", encodedToken },
                { "email", user.Email! }
            };

            string callbackLink = QueryHelpers.AddQueryString(model.ClientUri!, param!);

            await emailService.SendRegistrationConfirmationEmailAsync(user.Email!, callbackLink);

            return Ok(new { Message = "Email confirmation link has been resent." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
    
}

