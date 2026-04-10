using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Domain.Interfaces.Devices;

public interface IDeviceRepository
{
    Task<IDevice> AddAsync(Device device, IDeviceRelatedEntity relatedEntity, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(DeviceId deviceId, CancellationToken cancellationToken);
    
}