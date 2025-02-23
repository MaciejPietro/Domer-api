using Domer.Domain.Common;
using Domer.Domain.Enums.Projects;
using System;
using System.Collections.Generic;

namespace Domer.Domain.Interfaces.Projects;

public interface IProjectDetails
{
    ProjectDetailsId Id { get; set; }
    
    ProjectAdvertiserType AdvertiserType { get; set; }
    
    ProjectAdvertType AdvertType { get; set; }
    
    ProjectConditionType ConditionType { get; set; }
    
    ProjectMarketType MarketType { get; set; }
    
    ProjectOwnershipType OwnershipType { get; set; }
    
    ProjectType Type { get; set; }
    
    List<ExternalUrl>? Urls { get; set; }
    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}