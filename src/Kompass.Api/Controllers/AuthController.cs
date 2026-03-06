using Kompass.Application.Commands.Auth.ConfirmEmail;
using Kompass.Application.Commands.Auth.Login;
using Kompass.Application.Commands.Auth.Logout;
using Kompass.Application.Commands.Auth.Register;
using Kompass.Application.Commands.Auth.RemindPassword;
using Kompass.Application.Commands.Auth.ResendConfirmationEmail;
using Kompass.Application.Commands.Auth.ResetPassword;
using Kompass.Application.DTOs;
using Kompass.Application.DTOs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kompass.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpPost("confirmemail")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    
    [HttpPost("resendconfirmationemail")]
    [ProducesDefaultResponseType()]
    public async Task<IActionResult> ResendConfirmationEmail(ResendConfirmationEmailCommand command)
    {
        return Ok(await mediator.Send(command));
    }
    
    [HttpPost("remindpassword")]
    [ProducesDefaultResponseType()]
    public async Task<IActionResult> RemindPassword(RemindPasswordCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    
    [HttpPost("resetpassword")]
    [ProducesDefaultResponseType()]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        return Ok(await mediator.Send(command));
    }


    [HttpPost("login")]
    [ProducesDefaultResponseType(typeof(AuthResponseDTO))]
    public async Task<ActionResult<UserDto>> Login(LoginCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpPost("logout")]
    [ProducesDefaultResponseType()]
    [Authorize]
    public async Task<IActionResult> Logout(LogoutCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}

