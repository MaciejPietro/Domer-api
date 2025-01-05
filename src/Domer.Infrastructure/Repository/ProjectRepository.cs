using Domer.Application.Common.Responses;
using Domer.Domain.Entities.Projects;
using Domer.Domain.Interfaces.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Infrastructure.Repository;

public class ProjectRepository : IProjectRepository
{

    private readonly ApplicationDbContext _dbContext;
    
    public ProjectRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IProject> AddAsync(Project project, CancellationToken cancellationToken)
    {
        await _dbContext.Projects.AddAsync(project, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return project;
    }
    
    public async Task<(List<Project> Projects, int TotalCount)> GetAllAsync(
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Projects.AsNoTracking();
    
        var totalCount = await query.CountAsync(cancellationToken);
    
        var projects = await query
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (projects, totalCount);
    }
}