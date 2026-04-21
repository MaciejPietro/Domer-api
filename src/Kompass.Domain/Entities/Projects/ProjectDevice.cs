using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Interfaces.Projects;
using System;

namespace Kompass.Domain.Entities.Projects;

public class ProjectDevice : Entity<ProjectDeviceId>, IProjectDevice
{
    private ProjectDevice() {}
    
    public override ProjectDeviceId Id { get; protected set; } = Guid.CreateVersion7();
    public ProjectId ProjectId { get; private init; }
    public DeviceId DeviceId { get; private init; }

    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; private init; } = DateTime.UtcNow;

    public static ProjectDevice Create(ProjectId projectId, DeviceId deviceId)
    {
        ProjectDevice details = new() { ProjectId = projectId, DeviceId = deviceId };

        return details;
    }
}