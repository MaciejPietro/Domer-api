using Ardalis.Result;
using Kompass.Application.Commands.Project;
using Kompass.Application.Commands.Project.AttachDevice;
using Kompass.Application.Commands.Project.CreateProject;
using Kompass.Application.Commands.Project.DeleteProject;
using Kompass.Application.Commands.Project.UpdateProject;
using Kompass.Application.Commands.User.ResendEmailConfirmation;
using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Projects;
using Kompass.Application.Queries.Projects;
using Kompass.Application.Queries.Projects.GetAllProjects;
using Kompass.Application.Queries.Projects.GetProjectById;
using Kompass.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompass.Api.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController(IMediator mediator)
    : ControllerBase
{
    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetAllProjects([FromQuery] GetAllProjectsQuery query)
    {
        PaginatedResponse<ProjectListDto> result = await mediator.Send(query);
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetProject([FromRoute] string id)
    {
        GetProjectByIdQuery query = new (id);
        Result<ProjectDto> result = await mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpPost()]
    [Authorize]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
    {
        var result = await mediator.Send(command);
    
        return StatusCode(201, result);
    }
    
    [HttpPatch("{projectId}")]
    [Authorize]
    public async Task<IActionResult> UpdateProject([FromRoute] ProjectId projectId, [FromBody] UpdateProjectCommand command)
    {
        command.Id = projectId;
        
        return StatusCode(200, await mediator.Send(command));
    }

    
    [HttpDelete("{projectId}")]
    [Authorize]
    public async Task<IActionResult> DeleteProject([FromRoute] ProjectId projectId)
    {
        DeleteProjectCommand query = new (projectId);
        var result = await mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpPost("{projectId}/devices")]
    [Authorize]
    public async Task<IActionResult> AttachDevice([FromRoute] string projectId, [FromBody] AttachDeviceCommand command)
    {
        command.ProjectId = projectId;
        
        return StatusCode(200, await mediator.Send(command));
    }
}