using Domer.Domain.Common;
using Domer.Domain.Enums.Projects;
using System;
using System.Collections.Generic;

namespace Domer.Domain.Interfaces.Projects;

public interface IProjectDetails
{
    ProjectDetailsId Id { get; set; }
    
    int? UsableArea  { get; set; }
    int? BuildingArea  { get; set; }
    
    List<ExternalUrl>? Urls { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}