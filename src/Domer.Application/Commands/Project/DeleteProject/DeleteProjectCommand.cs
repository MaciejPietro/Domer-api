using Ardalis.Result;
using Domer.Domain.Common;
using MediatR;

namespace Domer.Application.Commands.Project.DeleteProject;


public record DeleteProjectCommand(ProjectId ProjectId) : IRequest<Result<Unit>>;