using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Enums.Projects;
using Kompass.Domain.Interfaces.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kompass.Domain.Entities.Projects;

public class Project : Entity<ProjectId>, IProject
{
    private Project() { }

    public override ProjectId Id { get; protected set; } = Guid.CreateVersion7();
    
    [MaxLength(100)]

    public string Name { get; private set; } = null!;

    [MaxLength(1000)]

    public string? Description { get; private set; }

    public ProjectStatus Status { get; private set; }
    
    public ProjectDetails ProjectDetails { get; private init; }
    
    public ProjectCreator ProjectCreator { get; private init; }
    
    public IReadOnlyCollection<Folder> Folders { get; private init; } = [];
    
    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; private init; } = DateTime.UtcNow;
    
    public IReadOnlyCollection<ProjectImage> Images { get; private init; } = [];

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name cannot be empty", nameof(name));
        Name = name;
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
    }

    public void UpdateStatus(ProjectStatus status)
    {
        Status = status;
    }

    public static Project Create(ProjectId id, string name, string? description, ProjectStatus status, ProjectDetails projectDetails,
        ProjectCreator projectCreator, List<ProjectImage>  projectImages)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name cannot be empty", nameof(name));

        Project project = new()
        {
            Id = id,
            Name = name,
            Description = description,
            Status = status,
            ProjectDetails = projectDetails ?? throw new ArgumentNullException(nameof(projectDetails)),
            ProjectCreator = projectCreator ?? throw new ArgumentNullException(nameof(projectCreator)),
            Images = projectImages
        };

        return project;
    }
}
