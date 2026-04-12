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
            
            var isSuccess = await _folderRepository.DeleteAsync(folderId, cancellationToken);
        
            return isSuccess ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Invalid();
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}
