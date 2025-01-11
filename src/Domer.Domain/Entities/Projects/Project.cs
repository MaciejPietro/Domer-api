using Domer.Domain.Common;
using Domer.Domain.Common.Entities;
using Domer.Domain.Enums.Projects;
using Domer.Domain.Interfaces.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domer.Domain.Entities.Projects;

public class Project : Entity<ProjectId>, IProject
{
    public override ProjectId Id { get; set; } = Guid.CreateVersion7();
    
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }

    public ProjectStatus Status { get; set; }
    
    public ProjectType Type { get; set; }
    
    public ProjectDetails ProjectDetails { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
}
