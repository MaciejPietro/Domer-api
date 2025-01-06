using Domer.Application.Common.Responses;
using Domer.Application.DTOs.Queries;
using Domer.Domain.Common;
using MediatR;

namespace Domer.Application.Queries.Projects.GetProjectById;

public record GetProjectByIdQuery(ProjectId Id) : IRequest<ResultResponse<ProjectDto>>;
