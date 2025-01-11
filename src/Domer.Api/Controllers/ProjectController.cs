using Domer.Application.Commands.Project;
using Domer.Application.Commands.Project.CreateProject;
using Domer.Application.Commands.Project.DeleteProject;
using Domer.Application.Commands.User.ResendEmailConfirmation;
using Domer.Application.Common.Responses;
using Domer.Application.DTOs.Queries;
using Domer.Application.Queries.Projects;
using Domer.Application.Queries.Projects.GetAllProjects;
using Domer.Application.Queries.Projects.GetProjectById;
using Domer.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domer.Api.Controllers;

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
        return StatusCode(201, await mediator.Send(command));
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