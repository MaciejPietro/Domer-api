using Kompass.Domain.Common;

namespace Kompass.Application.DTOs.Queries.Projects;

public class ProjectCreatorDto
{
    public ProjectCreatorId Id { get; set; }
    public string Config { get; set; }
}