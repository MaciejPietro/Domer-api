using Ardalis.Result;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Commands.Project.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Unit>>
{
    private readonly IValidator<CreateProjectCommand> _validator;
    
    public CreateProjectCommandHandler()
    {
    }
    
    public async Task<Result<Unit>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        // _validator.ValidateAndThrow(request);
        
        // If validation is successful, this code is reached
        Console.WriteLine(request.Name);

        // Your logic to handle the command goes here

        return Unit.Value;
    }
}
