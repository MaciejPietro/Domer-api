using Ardalis.Result;
using Kompass.Api.Common;
using Kompass.Application.Commands.Folder.CreateFolder;
using Kompass.Application.Commands.Folder.DeleteFolder;
using Kompass.Application.Commands.Folder.UpdateFolder;
using Kompass.Application.Commands.Project;
using Kompass.Application.Commands.Project.CreateProject;
using Kompass.Application.Commands.Project.DeleteProject;
using Kompass.Application.Commands.Project.UpdateProject;
using Kompass.Application.Commands.User.ResendEmailConfirmation;
using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Folders;
using Kompass.Application.Queries.Folders.GetAllFolders;
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

[Route("api/folder")]
[ApiController]
public class FolderController(IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllFolders([FromQuery] Guid projectId)
    {
        var query = new GetAllFoldersQuery
        {
            ProjectId = new ProjectId(projectId),
        };

        var result = await mediator.Send(query);

        return Ok(result);
    }
    
    
    [HttpPost()]
    [Authorize]
    public async Task<IActionResult> CreateFolder([FromBody] CreateFolderCommand command)
    {
        var result = await mediator.Send(command);
   
        return StatusCode(201, result);
    }
    
    [HttpPatch("{folderId}")]
    [Authorize]
    public async Task<IActionResult> UpdateFolder([FromRoute] string folderId, [FromBody] UpdateFolderCommand command)
    {
        command.Id = folderId;
        
        var result = await mediator.Send(command);
        
        return result.ToActionResult();

    }

    
    [HttpDelete("{folderId}")]
    [Authorize]
    public async Task<IActionResult> DeleteFolder([FromRoute] string folderId, [FromQuery] DeleteFolderCommand command)
    {
        
        command.Id = folderId;
        var result = await mediator.Send(command);
        
        return result.ToActionResult();
    }
}