using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Devices.Camera;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using Kompass.Domain.ValueObjects;
namespace Kompass.Application.Services.Devices.Factories;

public class CameraDeviceFactory : IDeviceFactory
{
    public (Device, IDeviceRelatedEntity) Create(string name, string? description)
    {
        var device = Device.Create(name, DeviceType.Camera, description);
        var camera = Camera.Create(device.Id, new Angle(0),new Angle(0),new Centimeter(200));

        return (device, camera);
    }
}