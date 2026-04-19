using Kompass.Domain.Common;
using Kompass.Domain.Enums.Projects;
using System;
using System.Collections.Generic;

namespace Kompass.Domain.Interfaces.Projects;

public interface IProjectDetails
{
    ProjectDetailsId Id { get; }

    List<ExternalUrl>? Urls { get; }

    DateTime CreatedAt { get; }

    DateTime UpdatedAt { get; }
}