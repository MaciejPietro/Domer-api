using Ardalis.Result;
using Domer.Domain.Common;
using Domer.Domain.Enums.Projects;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Domer.Application.Commands.Project.CreateProject;

public class CreateProjectCommand : IRequest<Result<Unit>>
{
    public required string Name { get; set; }
    
    public ProjectStatus Status { get; set; }
    
    public ProjectType Type { get; set; }
    
    public string? Description { get; set; }
    
    public ProjectAdvertiserType AdvertiserType { get; set; }
    
    public ProjectAdvertType AdvertType { get; set; }
    
    public ProjectConditionType ConditionType { get; set; }
    
    public ProjectMarketType MarketType { get; set; }
    
    public ProjectOwnershipType OwnershipType { get; set; }
    
    
    public List<ExternalUrl>? Urls { get; set; } = new();
    public List<IFormFile>? Images { get; set; } = new();
} 