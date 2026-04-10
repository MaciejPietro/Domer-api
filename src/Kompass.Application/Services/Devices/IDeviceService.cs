using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Services.Devices;

public interface IDeviceService
{
    Task<(Device device, IDeviceRelatedEntity RelatedEntity)> CreateDeviceAsync(
        DeviceType deviceType,
        string name,
        string? description,
        CancellationToken cancellationToken);
}
