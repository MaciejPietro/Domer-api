using Ardalis.Result;
using Kompass.Application.Common.Exceptions;
using Kompass.Domain.Enums.Projects;
using Kompass.Domain.Interfaces.Projects;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using InternalException = Kompass.Application.Common.Exceptions.InternalException;


namespace Kompass.Application.Commands.Project.DeleteProject;

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