using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Interfaces.Devices;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Infrastructure.Repository;

public class DeviceRepository(ApplicationDbContext dbContext) : IDeviceRepository
{
    public async Task<IDevice> AddAsync(Device device, CancellationToken cancellationToken)
    {
        await dbContext.Devices.AddAsync(device, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return device;
    }
}