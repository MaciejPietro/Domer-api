using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Enums.Projects;
using Kompass.Domain.Interfaces.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kompass.Domain.Entities.Projects;

public class Project : Entity<ProjectId>, IProject
{
    public override ProjectId Id { get; set; } = Guid.CreateVersion7();
    
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }

    public ProjectStatus Status { get; set; }
    
    public ProjectDetails ProjectDetails { get; set; }
    
    public ProjectCreator ProjectCreator { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
}
