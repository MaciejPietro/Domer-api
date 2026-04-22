using Ardalis.Result;
using Kompass.Domain.Common;
using MediatR;

namespace Kompass.Application.Commands.Project.AttachDevice;

public class AttachDeviceCommand: IRequest<Result<Unit>>
{
    public string? ProjectId { get; set; }
    
    public string? DeviceId { get; init; }
}