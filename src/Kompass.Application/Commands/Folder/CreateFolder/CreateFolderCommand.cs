using Ardalis.Result;
using Kompass.Domain.Common;
using MediatR;

namespace Kompass.Application.Commands.Folder.CreateFolder;

public class CreateFolderCommand : IRequest<Result<Unit>>
{
    public string? Name { get; init; }
    public string? ParentFolderId { get; init; }
    public string? ProjectId { get; init; }
} 