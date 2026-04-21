using Kompass.Domain.Common;
using System;


namespace Kompass.Domain.Interfaces.Projects;

public interface IProjectDevice
{
    ProjectDeviceId Id { get; }
    
    ProjectId ProjectId { get; }
    
    DeviceId DeviceId { get; }
    
    DateTime CreatedAt { get; }

    DateTime UpdatedAt { get; }
}