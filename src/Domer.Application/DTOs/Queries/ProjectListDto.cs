using Domer.Domain.Common;
using System;

namespace Domer.Application.DTOs.Queries;

public class ProjectListDto
{
    public ProjectId Id { get; set; }
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}