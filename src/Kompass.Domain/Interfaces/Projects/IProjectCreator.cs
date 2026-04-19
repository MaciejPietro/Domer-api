using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Enums.Projects;
using System;
using System.Collections.Generic;

namespace Kompass.Domain.Interfaces.Projects;

public interface IProjectCreator
{
    ProjectCreatorId Id { get; }
    
    ProjectId ProjectId { get; }
    
    string? Config { get; }

    DateTime CreatedAt { get; }

    DateTime UpdatedAt { get; }
}