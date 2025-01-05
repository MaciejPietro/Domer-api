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
    
    public async Task<List<Project>> GetAllAsync(int pageNumber, CancellationToken cancellationToken)
    {
        const int pageSize = 10;
        
        var result = await _dbContext.Projects.AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return result;
    }
}