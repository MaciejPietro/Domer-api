using Domer.Domain.Common;
using Domer.Domain.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Domain.Interfaces.Projects;

public interface IProjectRepository
{
        Task<IProject> AddAsync(Project project, ProjectDetails projectDetails, CancellationToken cancellationToken);

        Task UpdateAsync(Project project, ProjectDetails projectDetails, CancellationToken cancellationToken);

        
        Task<(List<Project> Projects, int TotalCount)> GetAllAsync(
            int pageNumber, 
            int pageSize, 
            CancellationToken cancellationToken);
        
        Task<Project> GetByIdAsync( ProjectId projectId,  CancellationToken cancellationToken);
        
        Task<bool> DeleteAsync( ProjectId projectId,  CancellationToken cancellationToken);

}