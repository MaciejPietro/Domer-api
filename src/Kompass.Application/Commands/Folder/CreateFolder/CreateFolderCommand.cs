using Ardalis.Result;
using Kompass.Domain.Common;
using MediatR;

namespace Kompass.Application.Commands.Folder.CreateFolder;

public class CreateFolderCommand : IRequest<Result<Unit>>
{
    public string? Name { get; set; }
    public string? ParentFolderId { get; set; }
    public object? ProjectId { get; set; }
} 