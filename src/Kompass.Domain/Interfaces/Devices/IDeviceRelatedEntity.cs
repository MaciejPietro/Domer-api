using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;

namespace Kompass.Domain.Interfaces.Devices;

public interface IDeviceRelatedEntity
{
    DeviceId DeviceId { get; }
    Device Device { get; init; }
}