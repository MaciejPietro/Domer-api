using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Responses;
using Domer.Domain.Common;
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
    public async Task<IProject> AddAsync(Project project, ProjectDetails projectDetails, CancellationToken cancellationToken)
    {
        await _dbContext.Projects.AddAsync(project, cancellationToken);
        
        await _dbContext.AddAsync(projectDetails, cancellationToken);
        
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
            .OrderByDescending(x => x.CreatedAt)  // Changed from OrderBy to OrderByDescending
            // .ThenByDescending(x => x.Id)          
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (projects, totalCount);
    }

    public async Task<Project> GetByIdAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects
            .Include(p => p.ProjectDetails)
            .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
        
        if (project == null)
        {
            throw new NotFoundException($"Nie znaleziono project o id {projectId}");
        }
        
        Console.WriteLine(project);
    
        return project;
    }

    public async Task<bool> DeleteAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
        
        if (project == null)
        {
            return false;
        }

        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync(cancellationToken);
    
        return true;
    }
}