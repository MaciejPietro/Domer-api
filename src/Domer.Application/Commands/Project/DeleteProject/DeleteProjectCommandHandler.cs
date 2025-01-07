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


namespace Domer.Application.Commands.Project.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result<Unit>>
{
    private readonly IProjectRepository _projectRepository;
    
    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<Result<Unit>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _projectRepository.DeleteAsync(request.ProjectId, cancellationToken);
        }
        catch (Exception e)
        {
            throw new InternalException(e.Message);
        }

        return Unit.Value;
    }
}