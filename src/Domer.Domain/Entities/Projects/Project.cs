using Domer.Domain.Common;
using Domer.Domain.Common.Entities;
using System;

namespace Domer.Domain.Entities.Projects;

public class Project : Entity<ProjectId>
{
    public override ProjectId Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; } = null!;
}