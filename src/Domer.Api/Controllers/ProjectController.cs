using Domer.Application.Commands.Project.CreateProject;
using Domer.Application.Commands.User.ResendEmailConfirmation;
using Domer.Application.DTOs.Queries;
using Domer.Application.Queries.Projects;
using Domer.Application.Queries.Projects.GetAllProjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> GetAllProjects(GetAllProjectsQuery query)
    {
        List<ProjectListDto> result = await mediator.Send(query);
        
        return Ok(result);
    }
    
    [HttpPost()]
    [Authorize]
    public async Task<ActionResult> CreateProject(CreateProjectCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}