using Ardalis.Result;
using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Projects;
using Kompass.Domain.Common;
using MediatR;

namespace Kompass.Application.Queries.Projects.GetProjectById;

public record GetProjectByIdQuery(string? Id) : IRequest<Result<ProjectDto>>;
