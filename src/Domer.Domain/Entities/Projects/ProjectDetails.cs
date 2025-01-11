using Domer.Domain.Common;
using Domer.Domain.Common.Entities;
using Domer.Domain.Enums.Projects;
using Domer.Domain.Interfaces.Projects;
using System;
using System.Collections.Generic;

namespace Domer.Domain.Entities.Projects;

public class ProjectDetails  : Entity<ProjectDetailsId>, IProjectDetails
{
    public override ProjectDetailsId Id { get; set; } = Guid.CreateVersion7();
    public ProjectId ProjectId { get; set; }
    public Project Project { get; set; }

    
    public int? UsableArea { get; set; }
    public int? BuildingArea { get; set; }

    public List<ExternalUrl>? Urls { get; set; } = new();


    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}