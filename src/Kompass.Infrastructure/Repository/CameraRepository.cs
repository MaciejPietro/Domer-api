using Kompass.Domain.Entities.Devices.Camera;
using Kompass.Domain.Interfaces.Devices.Camera;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Infrastructure.Repository;

public class CameraRepository(ApplicationDbContext dbContext): ICameraRepository
{
    public async Task<ICamera>  AddAsync(Camera device, CancellationToken cancellationToken)
    {
        await dbContext.Cameras.AddAsync(device, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return device;
    }
}