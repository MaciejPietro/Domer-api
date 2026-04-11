using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Infrastructure.Repository;

public class DeviceRepository(ApplicationDbContext dbContext) : IDeviceRepository
{
    public async Task<IDevice> AddAsync(Device device, IDeviceRelatedEntity relatedEntity, CancellationToken cancellationToken)
    {
        await dbContext.Devices.AddAsync(device, cancellationToken);
        await dbContext.AddAsync(relatedEntity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return device;
    }

    public async Task<(IEnumerable<IDevice>, int count)> GetAllAsync(DeviceType type, CancellationToken cancellationToken)
    {
        List<Device> devices = await dbContext.Devices
            .Where(d => d.Type == type)
            .ToListAsync(cancellationToken);

        return (devices.AsEnumerable(), devices.Count);
    }

    public async Task<bool> DeleteAsync(DeviceId deviceId, CancellationToken cancellationToken)
    {
        Device? device = await dbContext.Devices.FindAsync([deviceId], cancellationToken);

        if (device is null)
        {
            return false;
        }
        
        dbContext.Devices.Remove(device);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}