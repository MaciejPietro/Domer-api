using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Domain.Interfaces.Devices.Camera;

public interface ICameraRepository
{
    Task<ICamera> AddAsync(Entities.Devices.Camera.Camera device, CancellationToken cancellationToken);
    
}