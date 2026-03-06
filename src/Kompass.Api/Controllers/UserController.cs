using AutoMapper;
using Kompass.Application.Commands.User.DeleteUser;
using Kompass.Application.Commands.User.ResendEmailConfirmation;
using Kompass.Application.Commands.User.UpdateUser;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.Queries.User.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Kompass.Api.Controllers;

[Route("api/user")]
[ApiController]
public class UserController(IMediator mediator)
    : ControllerBase
{
    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        GetCurrentUserQuery query = new (User);
        
        UserDto result = await mediator.Send(query);
        
        return Ok(result);
    }
    
    [HttpPatch]
    [Authorize]
    public async Task<ActionResult> UpdateUser(UpdateUserCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpDelete()]
    [Authorize]
    public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
    {
        return Ok(await mediator.Send(command));
    }
    
    [HttpPost("resend-emailconfirmation")]
    [Authorize]
    public async Task<ActionResult> ResendEmailConfirmation(ResendEmailConfirmationCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}

