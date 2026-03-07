using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Enums.Projects;
using System;

namespace Kompass.Application.DTOs.Queries;

public class ProjectDto
{
    public ProjectId Id { get; set; }
    public string Name { get; set; }

    public ProjectStatus Status { get; set; }
    public string Description { get; set; }

    public ProjectDetailsDto? Details {get;set;}

    // public DateTime CreatedAt { get; set; }
    // public DateTime UpdatedAt { get; set; }
}