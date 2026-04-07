using Ardalis.Result;
using Kompass.Domain.Interfaces.Folders;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Kompass.Application.Commands.Folder.CreateFolder;

public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, Result<Unit>>
{
    private readonly IFolderRepository _folderRepository;
    
    public CreateFolderCommandHandler(IFolderRepository folderRepository)
    {
        _folderRepository = folderRepository;
    }
    
    public async Task<Result<Unit>> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid.TryParse(request.ParentFolderId, out Guid parentFolderId);
            Guid.TryParse(request.ProjectId, out Guid projectId);
            
            Domain.Entities.Folders.Folder newFolder = new()
            {
                Name = request.Name!, ProjectId = projectId, ParentFolderId = parentFolderId
            };
            
            await _folderRepository.AddAsync(newFolder, cancellationToken);

            
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}
