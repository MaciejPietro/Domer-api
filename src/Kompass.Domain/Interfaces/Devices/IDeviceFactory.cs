using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Interfaces.Devices;

namespace Kompass.Domain.Interfaces.Devices;

public interface IDeviceFactory<TConfig>
{
    (Device Device, IDeviceRelatedEntity RelatedEntity) Create(string name, string? description, TConfig config);
}