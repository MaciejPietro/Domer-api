using Kompass.Domain.Common;
using System.Collections.Generic;

namespace Kompass.Application.DTOs.Queries.Projects;

public class ProjectDetailsDto
{
    public ProjectDetailsId Id { get; set; }
    public List<ExternalUrl> Urls { get; set; } = new();
}