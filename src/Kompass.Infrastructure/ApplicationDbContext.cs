using Kompass.Application.Common;
using Kompass.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kompass.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options), IContext
{
    public DbSet<Domain.Entities.Projects.Project> Projects { get; init; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ProjectDetailsConfiguration).Assembly);

    }
}