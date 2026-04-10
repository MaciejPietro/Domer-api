using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using System;
using System.Collections.Generic;

namespace Kompass.Application.Services.Devices.Factories;

public class DeviceFactoryRegistry
{
    private readonly Dictionary<DeviceType, IDeviceFactory> _factories;

    public DeviceFactoryRegistry(CameraDeviceFactory cameraFactory)
    {
        _factories = new Dictionary<DeviceType, IDeviceFactory> { { DeviceType.Camera, cameraFactory } };
    }
    
    public IDeviceFactory GetFactory(DeviceType type)
    {
        return !_factories.TryGetValue(type, out IDeviceFactory? factory) ? throw new ArgumentException($"No factory for {type}") : factory;
    }
}