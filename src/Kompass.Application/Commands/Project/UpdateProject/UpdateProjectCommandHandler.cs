using Ardalis.Result;
using Kompass.Application.Common.Exceptions;
using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Enums.Projects;
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


namespace Kompass.Application.Commands.Project.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result<Unit>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IS3StorageService _s3StorageService;
    
    public UpdateProjectCommandHandler(IProjectRepository projectRepository, IS3StorageService s3StorageService)
    {
        _projectRepository = projectRepository;
        _s3StorageService = s3StorageService;
    }
    
    public async Task<Result<Unit>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Projects.Project? project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project == null)
            return Result<Unit>.Error($"{nameof(Domain.Entities.Projects.Project)} not found");

        if (request.Name is not null)
            project.UpdateName(request.Name);

        if (request.Description is not null)
            project.UpdateDescription(request.Description);

        if (request.Status is not null)
            project.UpdateStatus(request.Status.Value);

        if (request.Urls is not null)
        {
            var urls = request.Urls.Select(u => new ExternalUrl
            {
                Name = u.Name,
                Url = u.Url
            }).ToList();
            project.ProjectDetails.UpdateUrls(urls);
        }

        await _projectRepository.UpdateAsync(project, project.ProjectDetails, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
