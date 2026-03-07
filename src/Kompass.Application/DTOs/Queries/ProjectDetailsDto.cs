using Kompass.Domain.Common;
using System;
using System.Collections.Generic;

namespace Kompass.Application.DTOs.Queries;

public class ProjectDetailsDto
{
    // public ProjectDetailsId Id { get; set; }
    public List<ExternalUrl> Urls { get; set; } = new();
    // public DateTime CreatedAt { get; set; }
    // public DateTime UpdatedAt { get; set; }
}