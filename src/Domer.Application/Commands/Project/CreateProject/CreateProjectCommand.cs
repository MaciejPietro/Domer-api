using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Domer.Application.Commands.Project.CreateProject;

public class CreateProjectCommand : IRequest<Result<Unit>>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public int? BuildingArea { get; set; }
    public int? UsableArea { get; set; }
    
    public List<IFormFile> Images { get; set; } = new();
}