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
    private readonly IProjectRepository _projectRepository;
    private readonly IFolderRepository _folderRepository;
    private readonly IValidator<DeleteFolderCommand> _validator;
    
    public DeleteFolderCommandHandler(
        IProjectRepository projectRepository, 
        IFolderRepository folderRepository,
        IValidator<DeleteFolderCommand> validator)
    {
        _projectRepository = projectRepository;
        _folderRepository = folderRepository;
        _validator = validator;
    }
    
    public async Task<Result<Unit>> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid.TryParse(request.Id, out Guid folderId);
            
            await _folderRepository.DeleteAsync(folderId, cancellationToken);
        
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}
