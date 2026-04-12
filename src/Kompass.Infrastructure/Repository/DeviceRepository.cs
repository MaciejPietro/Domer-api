using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using Kompass.Domain.Interfaces.Devices.Camera;
using Microsoft.EntityFrameworkCore;
using System;
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

    public async Task<Device?> GetByIdAsync(DeviceId id, CancellationToken cancellationToken)
    {
        var device = await dbContext.Devices.FindAsync([id],  cancellationToken);
        
        return device;
    }
    
    public async Task<IDeviceRelatedEntity?> GetRelatedEntityById(DeviceType type, DeviceId deviceId, CancellationToken cancellationToken)
    {
        IDeviceRelatedEntity? relatedEntity = type switch
        {
            DeviceType.Camera => await dbContext.Cameras.FirstOrDefaultAsync(c => c.DeviceId == deviceId, cancellationToken),
            _ => null
        };
        
        return relatedEntity;
    }

    public async Task<(IEnumerable<IDevice>, int count)> GetAllAsync(DeviceType type, CancellationToken cancellationToken)
    {
        List<Device> devices = await dbContext.Devices
            .Where(d => d.Type == type)
            .ToListAsync(cancellationToken);

        return (devices.AsEnumerable(), devices.Count);
    }

    public async Task<bool> DeleteAsync(DeviceId id, CancellationToken cancellationToken)
    {
        Device? device = await dbContext.Devices.FindAsync([id], cancellationToken);
        
        Console.WriteLine(device);
        
        
        if (device is null)
        {
            return false;
        }
        
        dbContext.Devices.Remove(device);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}