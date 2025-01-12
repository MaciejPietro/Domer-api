using Ardalis.Result;
using Domer.Application.Common.Exceptions;
using Domer.Domain.Common;
using Domer.Domain.Entities.Projects;
using Domer.Domain.Enums.Projects;
using Domer.Domain.Interfaces;
using Domer.Domain.Interfaces.Projects;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InternalException = Domer.Application.Common.Exceptions.InternalException;


namespace Domer.Application.Commands.Project.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Unit>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IS3StorageService _s3StorageService;
    private readonly IValidator<CreateProjectCommand> _validator;
    
    public CreateProjectCommandHandler(
        IProjectRepository projectRepository, 
        IS3StorageService s3StorageService,
        IValidator<CreateProjectCommand> validator)
    {
        _projectRepository = projectRepository;
        _s3StorageService = s3StorageService;
        _validator = validator;
    }
    
    public async Task<Result<Unit>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
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
            var project = new Domain.Entities.Projects.Project
            {
                Name = request.Name, 
                Description = request.Description, 
                Status = request.Status, 
                Type = request.Type,
            };

            var projectDetails = new Domain.Entities.Projects.ProjectDetails
            {
                ProjectId = project.Id,
                UsableArea = request.UsableArea,
                BuildingArea = request.BuildingArea,
                Urls = request.Urls?.Select(l => new ExternalUrl() 
                { 
                    Name = l.Name, 
                    Url = l.Url 
                }).ToList() ?? new List<ExternalUrl>()
            };

            var projectCreator = new Domain.Entities.Projects.ProjectCreator
            {
                ProjectId = project.Id,  
            };

            await _projectRepository.AddAsync(project, projectDetails, projectCreator, cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}

            // Upload images and create ProjectImage entities
            // if (request.Images != null && request.Images.Any())
            // {
            //     foreach (var image in request.Images)
            //     {
            //         var uploadResult = await _s3StorageService.UploadObjectAsync(image);
            //         
            //         if (uploadResult.Success)
            //         {
            //             project.Images.Add(new ProjectImage
            //             {
            //                 FileName = uploadResult.FileName,
            //                 ImageUrl = $"https://your-bucket-url/{uploadResult.FileName}"
            //             });
            //         }
            //     }
            // }
