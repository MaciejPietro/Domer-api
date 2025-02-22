using Domer.Application.Common.Responses;
using Domer.Application.DTOs.Queries;
using Domer.Application.Heroes;
using Domer.Domain.Enums.Projects;
using MediatR;
using System.Collections.Generic;

namespace Domer.Application.Queries.Projects.GetAllProjects;

public class GetAllProjectsQuery : IRequest<PaginatedResponse<ProjectListDto>>
{

    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;

    public List<ProjectStatus>? Status { get; set; }

}