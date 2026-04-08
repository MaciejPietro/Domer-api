using Kompass.Domain.Common;
using Kompass.Domain.Enums.Devices;

namespace Kompass.Domain.Interfaces.Devices;

public interface IDevice
{
    DeviceId Id { get; }
    
    DeviceType Type { get; }
    string Name { get; }
    string? Description { get; }
}