using Ardalis.Result;
using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Projects;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InternalException = Kompass.Application.Common.Exceptions.InternalException;


namespace Kompass.Application.Commands.Project.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Unit>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IS3StorageService _s3StorageService;
    
    public CreateProjectCommandHandler(
        IProjectRepository projectRepository, 
        IS3StorageService s3StorageService)
    {
        _projectRepository = projectRepository;
        _s3StorageService = s3StorageService;
    }
    
    public async Task<Result<Unit>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var urls = request.Urls?.Select(l => new ExternalUrl
            {
                Name = l.Name,
                Url = l.Url
            }).ToList() ?? new List<ExternalUrl>();

            var projectId = Guid.CreateVersion7();

            var projectDetails = ProjectDetails.Create(projectId, urls);
            var projectCreator = ProjectCreator.Create(projectId, "{}");
            var projectImages = new List<ProjectImage>();

            var project = Domain.Entities.Projects.Project.Create(
                id: projectId,
                name: request.Name!,
                description: request.Description,
                status: request.Status,
                projectDetails: projectDetails,
                projectCreator: projectCreator,
                projectImages: projectImages
            );

            await _projectRepository.AddAsync(project, projectDetails, projectCreator, cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (ArgumentException ex)
        {
            return Result<Unit>.Error(ex.Message);
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }

}
