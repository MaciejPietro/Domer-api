using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Enums.Projects;
using Kompass.Domain.Interfaces.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kompass.Domain.Entities.Projects;

public class ProjectCreator : Entity<ProjectCreatorId>, IProjectCreator
{
    private ProjectCreator() {}
    public override ProjectCreatorId Id { get; protected set; } = Guid.CreateVersion7();
    
    public ProjectId ProjectId { get; private init; }
    
    [MaxLength(1000)]
    public string? Config { get; private init; } = "\"{\\\"floors\\\":[]}\"";

    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; private init; } = DateTime.UtcNow;
    
    public static ProjectCreator Create(ProjectId projectId, string config)
    {
        ProjectCreator creator = new ()
        {
            ProjectId = projectId,
            Config = config,
        };

        return creator;
    }
}