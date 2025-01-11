using Domer.Domain.Common;
using Domer.Domain.Common.Entities;
using System;

namespace Domer.Domain.Entities.Projects;

public class ProjectImage : Entity<ProjectImageId>
{
    public override ProjectImageId Id { get; set; } = Guid.CreateVersion7();
    
    // public ProjectId ProjectId { get; set; }
    public Project Project { get; set; }
    public string ImageUrl { get; set; }
    public string FileName { get; set; }
}

