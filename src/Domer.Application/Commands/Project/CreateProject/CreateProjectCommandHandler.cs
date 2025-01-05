using Ardalis.Result;
using Domer.Application.Common.Exceptions;
using Domer.Domain.Enums.Projects;
using Domer.Domain.Interfaces.Projects;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using InternalException = Domer.Application.Common.Exceptions.InternalException;


namespace Domer.Application.Commands.Project.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Unit>>
{
    private readonly IProjectRepository _projectRepository;
    
    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
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

            await _projectRepository.AddAsync(project, cancellationToken);
        }
        catch (Exception e)
        {
            throw new InternalException(e.Message);
        }

        return Unit.Value;
    }
}
