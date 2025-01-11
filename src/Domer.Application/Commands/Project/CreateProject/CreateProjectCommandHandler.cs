using Ardalis.Result;
using Domer.Application.Common.Exceptions;
using Domer.Domain.Entities.Projects;
using Domer.Domain.Enums.Projects;
using Domer.Domain.Interfaces;
using Domer.Domain.Interfaces.Projects;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InternalException = Domer.Application.Common.Exceptions.InternalException;


namespace Domer.Application.Commands.Project.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Unit>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IS3StorageService _s3StorageService;
    
    public CreateProjectCommandHandler(IProjectRepository projectRepository, IS3StorageService s3StorageService)
    {
        _projectRepository = projectRepository;
        _s3StorageService = s3StorageService;
    }
    
    public async Task<Result<Unit>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = new Domain.Entities.Projects.Project
            {
                Name = request.Name, 
                Description = request.Description, 
                Status = ProjectStatus.Design, 
                BuildingArea = request.BuildingArea,
                UsableArea = request.UsableArea 
            };
            
            // Upload images and create ProjectImage entities
            if (request.Images != null && request.Images.Any())
            {
                foreach (var image in request.Images)
                {
                    var uploadResult = await _s3StorageService.UploadObjectAsync(image);
                    
                    if (uploadResult.Success)
                    {
                        project.Images.Add(new ProjectImage
                        {
                            FileName = uploadResult.FileName,
                            ImageUrl = $"https://your-bucket-url/{uploadResult.FileName}"
                        });
                    }
                }
            }

            await _projectRepository.AddAsync(project, cancellationToken);
        }
        catch (Exception e)
        {
            throw new InternalException(e.Message);
        }

        return Unit.Value;
    }
}
