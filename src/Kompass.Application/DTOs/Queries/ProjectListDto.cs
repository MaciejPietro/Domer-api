using Kompass.Domain.Common;
using Kompass.Domain.Enums.Projects;
using System;

namespace Kompass.Application.DTOs.Queries;

public class ProjectListDto
{
    public ProjectId Id { get; set; }
    public string Name { get; set; }
    
    public ProjectStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}