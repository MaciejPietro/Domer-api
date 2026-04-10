using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using System;
using System.Collections.Generic;

namespace Kompass.Application.Services.Devices.Factories;

public class DeviceFactoryRegistry
{
    private readonly Dictionary<DeviceType, object> _factories;

    public DeviceFactoryRegistry(CameraDeviceFactory cameraFactory)
    {
        _factories = new Dictionary<DeviceType, object> { { DeviceType.Camera, cameraFactory } };
    }
    
    public IDeviceFactory<TConfig> GetFactory<TConfig>(DeviceType type)
    {
        if (!_factories.TryGetValue(type, out var factory))
            throw new ArgumentException($"No factory for {type}");

        return (IDeviceFactory<TConfig>)factory;
    }
}