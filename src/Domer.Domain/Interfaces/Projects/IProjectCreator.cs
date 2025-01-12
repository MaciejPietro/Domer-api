using Domer.Domain.Common;
using Domer.Domain.Entities.Projects;
using Domer.Domain.Enums.Projects;
using System;
using System.Collections.Generic;

namespace Domer.Domain.Interfaces.Projects;

public interface IProjectCreator
{
    ProjectCreatorId Id { get; set; }
    
    ProjectId ProjectId { get; set; }
    
    Project Project { get; set; }
    
    string config { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}