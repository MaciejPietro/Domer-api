using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Enums.Projects;
using System;
using System.Collections.Generic;

namespace Kompass.Domain.Interfaces.Projects;

public interface IProjectCreator
{
    ProjectCreatorId Id { get; set; }
    
    ProjectId ProjectId { get; set; }
    
    Project Project { get; set; }
    
    string config { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}