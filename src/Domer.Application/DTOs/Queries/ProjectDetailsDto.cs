using Domer.Domain.Common;
using System;
using System.Collections.Generic;

namespace Domer.Application.DTOs.Queries;

public class ProjectDetailsDto
{
    // public ProjectDetailsId Id { get; set; }
    public int UsableArea { get; set; }
    public int BuildingArea { get; set; }
    public List<ExternalUrl> Urls { get; set; }
    // public DateTime CreatedAt { get; set; }
    // public DateTime UpdatedAt { get; set; }
}