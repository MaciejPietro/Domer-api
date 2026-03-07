using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using System;

namespace Kompass.Domain.Entities.Projects;

public class ProjectImage : Entity<ProjectImageId>
{
    public override ProjectImageId Id { get; set; } = Guid.CreateVersion7();
    
    // public ProjectId ProjectId { get; set; }
    public Project Project { get; set; }
    public string ImageUrl { get; set; }
    public string FileName { get; set; }
}

