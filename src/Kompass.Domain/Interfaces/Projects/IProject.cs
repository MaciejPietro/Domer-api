using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Enums.Projects;
using System;

namespace Kompass.Domain.Interfaces.Projects;

public interface  IProject
{
    ProjectId Id { get; set; }
    string Name { get; set; }
    ProjectStatus Status { get; set; }
    
    ProjectDetails ProjectDetails { get; set; }

    string? Description { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}