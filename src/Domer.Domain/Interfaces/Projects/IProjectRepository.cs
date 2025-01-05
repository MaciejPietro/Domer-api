using Domer.Domain.Entities.Projects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Domain.Interfaces.Projects;

public interface IProjectRepository
{
        Task<IProject> AddAsync(Project project, CancellationToken cancellationToken);
        
        Task<(List<Project> Projects, int TotalCount)> GetAllAsync(
            int pageNumber, 
            int pageSize, 
            CancellationToken cancellationToken);

}