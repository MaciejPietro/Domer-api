using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Enums.Projects;
using Kompass.Domain.Interfaces.Projects;
using System;
using System.Collections.Generic;

namespace Kompass.Domain.Entities.Projects;

public class ProjectDetails  : Entity<ProjectDetailsId>, IProjectDetails
{
    private ProjectDetails() {}
    public override ProjectDetailsId Id { get; protected set; } = Guid.CreateVersion7();
    public ProjectId ProjectId { get; private init; }

    public List<ExternalUrl>? Urls { get; private set; } = [];

    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; private init; } = DateTime.UtcNow;

    public void UpdateUrls(List<ExternalUrl> urls)
    {
        Urls = urls;
    }

    public static ProjectDetails Create(ProjectId projectId, List<ExternalUrl>  urls)
    {
        ProjectDetails details = new() { ProjectId = projectId, Urls = urls, };

        return details;
    }
}