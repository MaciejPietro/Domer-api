using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Enums.Devices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Domain.Interfaces.Devices;

public interface IDeviceRepository
{
    Task<IDevice> AddAsync(Device device, IDeviceRelatedEntity relatedEntity, CancellationToken cancellationToken);
    
    Task<(IEnumerable<IDevice>, int count)> GetAllAsync(DeviceType type, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(DeviceId deviceId, CancellationToken cancellationToken);
    
}