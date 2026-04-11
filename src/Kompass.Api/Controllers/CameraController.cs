using Kompass.Application.Commands.Device.Camera;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Devices.Cameras;
using Kompass.Application.Queries.Devices.Camera.GetAllCameras;
using Kompass.Application.Queries.Folders.GetAllFolders;
using Kompass.Application.Queries.Projects.GetAllProjects;
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
    [HttpPost()]
    [Authorize]
    public async Task<IActionResult> CreateCamera([FromBody] CreateCameraCommand command)
    {
        var result = await mediator.Send(command);
    
        // Success case
        return StatusCode(201, result);
    }

    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetAllCameras([FromQuery] GetAllCamerasQuery query)
    {
         List<CameraListDto> result = await mediator.Send(query);

         return Ok(result);
    }
}