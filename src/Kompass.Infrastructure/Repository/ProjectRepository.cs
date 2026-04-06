using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Enums.Projects;
using Kompass.Domain.Interfaces.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Infrastructure.Repository;

public class ProjectRepository(ApplicationDbContext dbContext) : IProjectRepository
{
    public async Task<IProject> AddAsync(Project project, ProjectDetails projectDetails, IProjectCreator projectCreator, CancellationToken cancellationToken)
    {
        await dbContext.Projects.AddAsync(project, cancellationToken);
        
        await dbContext.AddAsync(projectDetails, cancellationToken);

        await dbContext.AddAsync(projectCreator, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return project;
    }

    public async Task UpdateAsync(Project project, ProjectDetails projectDetails, CancellationToken cancellationToken)
    {
    
        Project? existingProject = await GetByIdAsync(project.Id, cancellationToken);


        // Detach existing entities to avoid tracking conflicts
        dbContext.Entry(existingProject).State = EntityState.Detached;
        dbContext.Entry(existingProject!.ProjectDetails).State = EntityState.Detached;

        // Update Project entity
        dbContext.Entry(project).State = EntityState.Modified;

        // Update ProjectDetails entity
        dbContext.Entry(projectDetails).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("An error occurred while updating the project", ex);
        }
    }

    public async Task<(List<Project> Projects, int TotalCount)> GetAllAsync(
        int pageNumber, 
        int pageSize, 
        List<ProjectStatus>? statuses,
        CancellationToken cancellationToken)
    {
        IQueryable<Project> query = dbContext.Projects.AsNoTracking();
        
        if (statuses != null)
        {
            query = query.Where(p => statuses.Contains(p.Status));
        }
    
        int totalCount = await query.CountAsync(cancellationToken);
        
    
        List<Project> projects = await query
            .OrderByDescending(x => x.CreatedAt)  // Changed from OrderBy to OrderByDescending
            // .ThenByDescending(x => x.Id)          
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (projects, totalCount);
    }

    public async Task<Project?> GetByIdAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects
            .Include(p => p.ProjectDetails)
            .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
        
       
    
        return project;
    }

    public async Task<bool> DeleteAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
        
        if (project == null)
        {
            return false;
        }

        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync(cancellationToken);
    
        return true;
    }
}