using Ardalis.Result;
using FluentValidation;
using Kompass.Application.Common.Exceptions;
using Kompass.Domain.Interfaces.Folders;
using Kompass.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Folder.UpdateFolder;

public class UpdateFolderCommandHandler : IRequestHandler<UpdateFolderCommand, Result<Unit>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IFolderRepository _folderRepository;
    private readonly IValidator<UpdateFolderCommand> _validator;
    
    public UpdateFolderCommandHandler(
        IProjectRepository projectRepository, 
        IFolderRepository folderRepository,
        IValidator<UpdateFolderCommand> validator)
    {
        _projectRepository = projectRepository;
        _folderRepository = folderRepository;
        _validator = validator;
    }
    
    public async Task<Result<Unit>> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid.TryParse(request.Id, out var folderId);
            string folderName = request.Name!;

            Domain.Entities.Folders.Folder? existingFolder = await _folderRepository.GetByIdAsync(folderId, cancellationToken);

            if (existingFolder is null)
            {
                throw new InternalException("Folder not found");
            }
            
            existingFolder.Name = request.Name!;
            
            await _folderRepository.UpdateAsync(existingFolder, cancellationToken);
        
            
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}
