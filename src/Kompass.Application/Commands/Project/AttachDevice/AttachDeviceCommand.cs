using Ardalis.Result;
using Kompass.Domain.Common;
using MediatR;

namespace Kompass.Application.Commands.Project.AttachDevice;

public class AttachDeviceCommand: IRequest<Result<Unit>>
{
    public ProjectId ProjectId { get; set; }
    
    public DeviceId DeviceId { get; init; }
}