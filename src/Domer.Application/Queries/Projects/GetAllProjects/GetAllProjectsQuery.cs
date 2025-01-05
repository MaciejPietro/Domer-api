using Domer.Application.Common.Responses;
using Domer.Application.DTOs.Queries;
using Domer.Application.Heroes;
using MediatR;
using System.Collections.Generic;

namespace Domer.Application.Queries.Projects.GetAllProjects;

public class GetAllProjectsQuery : IRequest<List<ProjectListDto>>
{

    public GetAllProjectsQuery()
    {
    }
}