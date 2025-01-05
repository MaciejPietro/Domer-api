using Ardalis.Result;
using MediatR;

namespace Domer.Application.Commands.Project.CreateProject;

public class CreateProjectCommand : IRequest<Result<Unit>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public int? BuildingArea { get; set; }
    public int? UsableArea { get; set; }
}