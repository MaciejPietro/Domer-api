using Ardalis.Result;
using Kompass.Domain.Common;
using MediatR;

namespace Kompass.Application.Commands.Project.DeleteProject;


public record DeleteProjectCommand(ProjectId ProjectId) : IRequest<Result<Unit>>;