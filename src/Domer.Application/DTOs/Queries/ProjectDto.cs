using Domer.Domain.Common;
using Domer.Domain.Enums.Projects;
using System;

namespace Domer.Application.DTOs.Queries;

public class ProjectDto
{
    public ProjectId Id { get; set; }
    public string Name { get; set; }

    public ProjectStatus Status { get; set; }
    public string Description { get; set; }

    public int? UsableArea {get;set;}
    public int? BuildingArea {get;set;}

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}