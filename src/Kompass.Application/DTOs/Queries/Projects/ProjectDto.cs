using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Enums.Projects;

namespace Kompass.Application.DTOs.Queries.Projects;

public class ProjectDto
{
    public ProjectId Id { get; init; }
    public string Name { get; init; }

    public ProjectStatus Status { get; init; }
    public string? Description { get; init; }
    public ProjectDetailsDto? Details {get; init;}
    
    public ProjectCreatorDto Creator { get; init; }
}