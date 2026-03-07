using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries;
using Kompass.Domain.Common;
using MediatR;

namespace Kompass.Application.Queries.Projects.GetProjectById;

public record GetProjectByIdQuery(ProjectId Id) : IRequest<ResultResponse<ProjectDto>>;
