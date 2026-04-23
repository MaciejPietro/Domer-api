using Ardalis.Result;
using FluentValidation;
using Kompass.Domain.Interfaces.Folders;
using Kompass.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Folder.DeleteFolder;

public class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, Result<Unit>>
{
    private readonly IFolderRepository _folderRepository;
    
    public DeleteFolderCommandHandler(
        IFolderRepository folderRepository)
    {
        _folderRepository = folderRepository;
    }
    
    public async Task<Result<Unit>> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid.TryParse(request.Id, out Guid folderId);
            
            var folder = await _folderRepository.DeleteAsync(folderId, cancellationToken);
            
            if (folder is null)
            {
                return Result.NotFound($"Folder with ID {folderId} was not found");
            }
        
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}
