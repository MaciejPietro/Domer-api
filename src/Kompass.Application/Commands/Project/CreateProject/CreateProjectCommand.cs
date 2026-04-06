using Ardalis.Result;
using Kompass.Domain.Common;
using Kompass.Domain.Enums.Projects;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Kompass.Application.Commands.Project.CreateProject;

public class CreateProjectCommand : IRequest<Result<Unit>>
{
    public string? Name { get; init; }
    public ProjectStatus Status { get; init; }
    public string? Description { get; init; }
    public List<ExternalUrl>? Urls { get; init; } = new();
    public List<IFormFile>? Images { get; init; } = new();
} 