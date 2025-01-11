using Domer.Domain.Common;
using Domer.Domain.Entities.Projects;
using Domer.Domain.Enums.Projects;
using System;

namespace Domer.Application.DTOs.Queries;

public class ProjectDto
{
    public ProjectId Id { get; set; }
    public string Name { get; set; }

    public ProjectType Type { get; set; }

    public ProjectStatus Status { get; set; }
    public string Description { get; set; }

    public ProjectDetailsDto? Details {get;set;}

    // public DateTime CreatedAt { get; set; }
    // public DateTime UpdatedAt { get; set; }
}