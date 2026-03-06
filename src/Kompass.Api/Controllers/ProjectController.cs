using Ardalis.Result;
using Kompass.Application.Commands.Project;
using Kompass.Application.Commands.Project.CreateProject;
using Kompass.Application.Commands.Project.DeleteProject;
using Kompass.Application.Commands.Project.UpdateProject;
using Kompass.Application.Commands.User.ResendEmailConfirmation;
using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries;
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
    public async Task<ActionResult> GetAllProjects([FromQuery] GetAllProjectsQuery query)
    {
        PaginatedResponse<ProjectListDto> result = await mediator.Send(query);
        
        return Ok(result);
    }
    
    [HttpGet("{projectId}")]
    [Authorize]
    public async Task<ActionResult> GetProjectById([FromRoute] ProjectId projectId)
    {
        GetProjectByIdQuery query = new (projectId);
        ResultResponse<ProjectDto> result = await mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpPost()]
    [Authorize]
    public async Task<ActionResult> CreateProject([FromBody] CreateProjectCommand command)
    {
        var result = await mediator.Send(command);
    
        if (result.Status == ResultStatus.Invalid)
        {
            // Return 400 Bad Request with validation errors
            return BadRequest(new 
            { 
                Errors = result.ValidationErrors
            });
        }
    
        if (result.Status == ResultStatus.Error)
        {
            // Return 500 Internal Server Error
            return StatusCode(500, new 
            { 
                Error = result.Errors.FirstOrDefault()
            });
        }

        // Success case
        return StatusCode(201, result);
    }
    
    [HttpPatch("{projectId}")]
    [Authorize]
    public async Task<ActionResult> UpdateProject([FromRoute] ProjectId projectId, [FromBody] UpdateProjectCommand command)
    {
        command.Id = projectId;
        
        return StatusCode(200, await mediator.Send(command));
    }

    
    [HttpDelete("{projectId}")]
    [Authorize]
    public async Task<ActionResult> DeleteProject([FromRoute] ProjectId projectId)
    {
        
        DeleteProjectCommand query = new (projectId);
        var result = await mediator.Send(query);
    
        return Ok(result);
    }
}