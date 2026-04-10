using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Devices.Camera;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using Kompass.Domain.ValueObjects;
using Kompass.Domain.ValueObjects.Device;

namespace Kompass.Application.Services.Devices.Factories;

public class CameraDeviceFactory : IDeviceFactory<CameraConfiguration>
{
    public (Device, IDeviceRelatedEntity) Create(string name, string? description, CameraConfiguration config)
    {
        var device = Device.Create(name, DeviceType.Camera, description);
        var camera = Camera.Create(device.Id, config.VerticalAngle, config.HorizontalAngle, config.MaxDistance);

        return (device, camera);
    }
}