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
    private readonly IFolderRepository _folderRepository;
    
    public UpdateFolderCommandHandler(
        IFolderRepository folderRepository)
    {
        _folderRepository = folderRepository;
    }
    
    public async Task<Result<Unit>> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid.TryParse(request.Id, out var folderId);

            Domain.Entities.Folders.Folder? existingFolder = await _folderRepository.GetByIdAsync(folderId, cancellationToken);

            if (existingFolder is null)
            {
                return Result<Unit>.Error("Folder not found");
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
