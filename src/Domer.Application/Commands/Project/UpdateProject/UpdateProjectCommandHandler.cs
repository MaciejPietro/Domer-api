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


namespace Domer.Application.Commands.Project.UpdateProject;

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
        Domain.Entities.Projects.Project project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if(request.Name is not null ) project.Name = request.Name;
        if(project.Description is not null ) project.Description = request.Description;
        if(request.Status is not null ) project.Status = request.Status.Value;
        // if(request.Type is not null ) project.Type = request.Type.Value;


        ProjectDetails projectDetails = project.ProjectDetails;
        
        // if(request.UsableArea is not null) projectDetails.UsableArea = request.UsableArea;
        // if(request.BuildingArea is not null) projectDetails.BuildingArea = request.BuildingArea;
        if(request.Urls is not null) projectDetails.Urls = request.Urls?.Select(u => new ExternalUrl
        {
            Name = u.Name,
            Url = u.Url
        }).ToList() ?? new List<ExternalUrl>();
        
        await _projectRepository.UpdateAsync(project, projectDetails, cancellationToken);
        

        return Unit.Value;
    }
}
