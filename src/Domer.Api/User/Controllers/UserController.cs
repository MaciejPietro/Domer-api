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
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Domer.Api.User.Controllers;

[Route("api/user")]
[ApiController]
public class UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService  emailService, IMapper mapper)
    : ControllerBase
{

    [HttpGet("current")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        ApplicationUser? currentUser = await userManager.GetUserAsync(User);

        if (currentUser == null)
        {
            return Unauthorized(); 
        }

        var userDto = new UserDto
        {
            Id = currentUser.Id,
            Email = currentUser.Email,
        };

        return Ok(userDto);
    }

    [HttpPatch()]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUser model)
    {
        ApplicationUser? user = await userManager.FindByIdAsync(model.Id);
        
        if (user == null)
        {
            return BadRequest();
        }
        
        IdentityResult setEmailResult = await userManager.SetEmailAsync(user, model.Email);
        IdentityResult setUserNameResult = await userManager.SetUserNameAsync(user, model.Email);

            
        if (!setEmailResult.Succeeded || !setUserNameResult.Succeeded)
        {
            return BadRequest();
        }
        
         
        await userManager.UpdateAsync(user);

        UserDto? responseDto = mapper.Map<UserDto>(user);
                
        return Ok(responseDto);
        
    }
}

