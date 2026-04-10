using Kompass.Application.Commands.Device.Camera;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kompass.Api.Controllers;

[Route("api/cameras")]
[ApiController]
public class CameraController(IMediator mediator) : ControllerBase
{
    [HttpPost()]
    [Authorize]
    public async Task<IActionResult> CreateCamera([FromBody] CreateCameraCommand command)
    {
        var result = await mediator.Send(command);
    
        // Success case
        return StatusCode(201, result);
    }
}