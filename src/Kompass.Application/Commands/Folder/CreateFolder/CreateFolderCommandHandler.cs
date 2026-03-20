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
    private readonly IProjectRepository _projectRepository;
    private readonly IFolderRepository _folderRepository;
    private readonly IValidator<CreateFolderCommand> _validator;
    
    public CreateFolderCommandHandler(
        IProjectRepository projectRepository, 
        IFolderRepository folderRepository,
        IValidator<CreateFolderCommand> validator)
    {
        _projectRepository = projectRepository;
        _folderRepository = folderRepository;
        _validator = validator;
    }
    
    public async Task<Result<Unit>> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("test");
        Console.WriteLine(request);
        // Perform validation
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors
                .Select(x => new ValidationError
                {
                    ErrorMessage = x.ErrorMessage,
                    Identifier = x.PropertyName
                });
            return Result<Unit>.Invalid(validationErrors);
        }

        try
        {

            // Guid.TryParse(request.ParentFolderId, out var parentFolderId);
            // Guid.TryParse(request.ProjectId, out var projectId);
            //
            // var newFolder = new Domain.Entities.Folders.Folder
            // {
            //     Name = request.Name!, ProjectId = projectId, ParentFolderId = parentFolderId
            // };
            //
            // await _folderRepository.AddAsync(newFolder, cancellationToken);

            
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
        
        

        // try
        // {
        //     // Convert string IDs to strongly-typed IDs
        //     var projectId = new ProjectId(Guid.Parse(request.ProjectId as string ?? string.Empty));
        //     FolderId? parentFolderId = null;
        //
        //     if (request.ParentFolderId is string parentFolderIdStr && !string.IsNullOrEmpty(parentFolderIdStr))
        //     {
        //         parentFolderId = new FolderId(Guid.Parse(parentFolderIdStr));
        //     }
        //
        //     var folder = new Domain.Entities.Folders.Folder
        //     {
        //         Name = request.Name!,
        //         ParentFolderId = parentFolderId,
        //         ProjectId = projectId,
        //     };
        //
        //     await _folderRepository.AddAsync(folder, cancellationToken);
        //     return Result<Unit>.Success(Unit.Value);
        // }
        // catch (Exception e)
        // {
        //     return Result<Unit>.Error(e.Message);
        // }
    }
}
