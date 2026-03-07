using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries;
using Kompass.Domain.Enums.Projects;
using MediatR;
using System.Collections.Generic;

namespace Kompass.Application.Queries.Projects.GetAllProjects;

public class GetAllProjectsQuery : IRequest<PaginatedResponse<ProjectListDto>>
{

    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;

    public List<ProjectStatus>? Status { get; set; }

}