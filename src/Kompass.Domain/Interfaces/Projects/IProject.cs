using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Enums.Projects;
using System;

namespace Kompass.Domain.Interfaces.Projects;

public interface  IProject
{
    ProjectId Id { get; }
    string Name { get;  }
    ProjectStatus Status { get;  }
    
    ProjectDetails ProjectDetails { get; }

    string? Description { get;  }

    DateTime CreatedAt { get;  }

    DateTime UpdatedAt { get; }
    
}