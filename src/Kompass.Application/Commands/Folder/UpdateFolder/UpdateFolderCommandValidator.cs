using FluentValidation;
using Kompass.Application.Commands.Folder.CreateFolder;
using Kompass.Application.Common.Validation;
using Kompass.Domain.Interfaces.Folders;
using Kompass.Domain.Interfaces.Projects;
using System;
using System.Threading;

namespace Kompass.Application.Commands.Folder.UpdateFolder;

public class UpdateFolderCommandValidator : AbstractValidator<UpdateFolderCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IFolderRepository _folderRepository;

    public UpdateFolderCommandValidator(
        IProjectRepository projectRepository,
        IFolderRepository folderRepository)
    {
        _projectRepository = projectRepository;
        _folderRepository = folderRepository;
        
        // NAME
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        
        // ID
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeGuidObject().When(x => !string.IsNullOrEmpty(x.Id))
            .MustFolderExists(_folderRepository).When(x => !string.IsNullOrEmpty(x.Id));
        
        // DUPLICATED NAME
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustFolderHaveNoDuplicatedNames( async  (x) =>
            {
                Guid.TryParse(x.Id, out Guid folderId);
                Domain.Entities.Folders.Folder? folder =  await _folderRepository.GetByIdAsync(folderId, CancellationToken.None);
                
                return folder?.ParentFolderId.ToString();
            },x => x.Name!, _folderRepository);
    }
}