using Ardalis.Result;
using Kompass.Application.Commands.Device.Camera.CreateCamera;
using Kompass.Application.Commands.Device.Camera.DeleteCamera;
using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries.Devices.Cameras;
using Kompass.Application.Queries.Devices.Camera.GetAllCameras;
using Kompass.Application.Queries.Devices.Camera.GetCameraById;
using Kompass.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kompass.Api.Controllers;

[Route("api/cameras")]
[ApiController]
public class CameraController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCamera([FromBody] CreateCameraCommand command)
    {
        var result = await mediator.Send(command);
    
        // Success case
        return StatusCode(201, result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCameras([FromRoute] GetAllCamerasQuery query)
    {
         List<CameraListDto> result = await mediator.Send(query);

         return Ok(result);
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCamera([FromRoute] string id)
    {
        GetCameraByIdQuery query = new (id);
        Result<CameraDto> result = await mediator.Send(query);

        return Ok(result);
    }
    
    [HttpDelete("{deviceId}")]
    [Authorize]
    public async Task<IActionResult> DeleteCamera([FromRoute] string deviceId, [FromQuery] DeleteCameraCommand command)
    {
        command.Id = deviceId;
        Result<Unit> result = await mediator.Send(command);
        return Ok(result);
    }
}