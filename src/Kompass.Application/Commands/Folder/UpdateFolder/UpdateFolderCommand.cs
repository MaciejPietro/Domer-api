using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Folder.UpdateFolder;

public class UpdateFolderCommand : IRequest<Result<Unit>>
{
    public string? Name { get; init; }
    public string? Id { get; set; }
} 