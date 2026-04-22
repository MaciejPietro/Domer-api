using Ardalis.Result;
using Kompass.Application.DTOs.Queries.Projects;
using Kompass.Domain.Entities.Projects;
using MediatR;
using System.Collections.Generic;

namespace Kompass.Application.Queries.Projects.GetProjectDevices;

public class GetProjectDevicesQuery : IRequest<Result<List<ProjectDevice>>>
{
    public string? Id { get; set; }
}
