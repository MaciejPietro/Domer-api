using Domer.Application.Commands.Project.CreateProject;
using Domer.Application.Commands.User.ResendEmailConfirmation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Domer.Api.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController(IMediator mediator)
    : ControllerBase
{
    [HttpPost()]
    [Authorize]
    public async Task<ActionResult> CreateProject(CreateProjectCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}