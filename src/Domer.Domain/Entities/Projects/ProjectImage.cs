using Domer.Domain.Common;

namespace Domer.Domain.Entities.Projects;

public class ProjectImage : BaseEntity
{
    public ProjectId ProjectId { get; set; }
    public Project Project { get; set; }
    public string ImageUrl { get; set; }
    public string FileName { get; set; }
}

public class BaseEntity
{
}