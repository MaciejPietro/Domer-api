using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Folder.DeleteFolder;

public class DeleteFolderCommand : IRequest<Result<Unit>>
{
    public string? Id {get; set;}
}