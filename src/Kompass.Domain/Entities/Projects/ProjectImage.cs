using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using System;

namespace Kompass.Domain.Entities.Projects;

// NOT USED FOR NOW

public class ProjectImage : Entity<ProjectImageId>
{
    public override ProjectImageId Id { get; protected set; } = Guid.CreateVersion7();
    
    public ProjectId ProjectId { get; private init; }
    public string ImageUrl { get; private init; }
    public string FileName { get; private init; }
}

