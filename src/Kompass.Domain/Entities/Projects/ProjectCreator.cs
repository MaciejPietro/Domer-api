using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Enums.Projects;
using Kompass.Domain.Interfaces.Projects;
using System;
using System.Collections.Generic;

namespace Kompass.Domain.Entities.Projects;

public class ProjectCreator : Entity<ProjectCreatorId>, IProjectCreator
{
    public override ProjectCreatorId Id { get; set; } = Guid.CreateVersion7();
    
    public ProjectId ProjectId { get; set; }
    
    public Project Project { get; set; }
    
    public string config { get; set; } = "\"{\\\"floors\\\":[]}\"";


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}