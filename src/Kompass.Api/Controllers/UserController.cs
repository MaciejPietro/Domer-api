using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Kompass.Application.Commands.User.DeleteUser;
using Kompass.Application.Commands.User.ResendEmailConfirmation;
using Kompass.Application.Commands.User.UpdateUser;
using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Users;
using Kompass.Application.Queries.User.GetCurrentUser;
using Kompass.Application.Queries.Users.GetUsers;
using Kompass.Domain.Enums.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Kompass.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IMediator mediator)
    : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        GetCurrentUserQuery query = new (User);
        
        UserDto result = await mediator.Send(query);
        
        return Ok(result);
    }
    
    [HttpGet()]
    [Authorize(Roles=nameof(UserRole.Admin))]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
    {
        PaginatedResponse<UserListDto> result = await mediator.Send(query);

        
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

