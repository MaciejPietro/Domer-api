using Ardalis.Result;
using FluentValidation;
using Kompass.Application.Commands.Project.CreateProject;
using Kompass.Domain.Common;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Folders;
using Kompass.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Guid.TryParse(request.ParentFolderId, out var parentFolderId);
            Guid.TryParse(request.ProjectId, out var projectId);
            
            var newFolder = new Domain.Entities.Folders.Folder
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
