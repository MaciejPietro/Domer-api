using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Documents;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Domain.Interfaces.Devices;

public interface IDeviceRepository
{
    Task<IDevice> AddAsync(Device device, CancellationToken cancellationToken);
}