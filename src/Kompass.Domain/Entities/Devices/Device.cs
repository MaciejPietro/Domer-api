using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kompass.Domain.Entities.Devices;

public class Device: Entity<DeviceId>, IDevice
{
    
    private Device() {}
    public override DeviceId Id { get; protected set; } = Guid.CreateVersion7();

    public DeviceType Type { get; init; } = DeviceType.Camera;
    
    [MaxLength(255)]
    public string Name { get; init; }
    
    [MaxLength(1000)] 
    public string? Description { get; init; } = string.Empty;  
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;

    public static Device Create(string name, DeviceType type, string? description)
    {
        Device device = new() { Name = name, Type = type, Description = description };

        return device;
    }
}