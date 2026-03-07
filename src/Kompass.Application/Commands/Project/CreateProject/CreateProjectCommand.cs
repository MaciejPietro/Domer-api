using Ardalis.Result;
using Kompass.Domain.Common;
using Kompass.Domain.Enums.Projects;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Kompass.Application.Commands.Project.CreateProject;

public class CreateProjectCommand : IRequest<Result<Unit>>
{
    public required string Name { get; set; }
    public ProjectStatus Status { get; set; }
    public string? Description { get; set; }
    public List<ExternalUrl>? Urls { get; set; } = new();
    public List<IFormFile>? Images { get; set; } = new();
} 