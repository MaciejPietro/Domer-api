using Domer.Domain.Common;
using Domer.Domain.Enums.Projects;
using System;

namespace Domer.Domain.Interfaces.Projects;

public interface  IProject
{
    ProjectId Id { get; set; }
    string Name { get; set; }
    ProjectStatus Status { get; set; }
    string? Description { get; set; }
    int? BuildingArea { get; set; }
    int? UsableArea { get; set; }
    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}