using Kompass.Application.Services.Devices.Factories;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Services.Devices;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly DeviceFactoryRegistry _registry;

    public DeviceService(IDeviceRepository deviceRepository, DeviceFactoryRegistry registry)
    {
        _deviceRepository = deviceRepository;
        _registry = registry;
    }


    public async Task<(Device device, IDeviceRelatedEntity RelatedEntity)> CreateDeviceAsync<TConfig>(
        DeviceType deviceType,
        string name,
        string? description,
        TConfig? config,
        CancellationToken cancellationToken)
    {
        IDeviceFactory<TConfig> factory = _registry.GetFactory<TConfig>(deviceType);  
        (Device device, IDeviceRelatedEntity relatedEntity) = factory.Create(name, description, config);

        await _deviceRepository.AddAsync(device, relatedEntity, cancellationToken);

        return (device, relatedEntity);
    }
    
}