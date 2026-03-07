using Ardalis.Result;
using Kompass.Domain.Common;
using Kompass.Domain.Enums.Projects;
using MediatR;
using System.Collections.Generic;

namespace Kompass.Application.Commands.Project.UpdateProject;

public class UpdateProjectCommand : IRequest<Result<Unit>>
{
    public ProjectId Id { get; set; }

    public string? Name { get; set; }
    
    public ProjectStatus? Status { get; set; }
    
    public string? Description { get; set; }
    
    public List<ExternalUrl>? Urls { get; set; } = new();
}