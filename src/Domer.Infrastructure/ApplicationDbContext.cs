using Domer.Application.Common;
using Domer.Infrastructure.Configurations;
using Domer.Infrastructure.Hero;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Domer.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options), IContext
{
    public DbSet<Domain.Heroes.Entities.Hero> Heroes { get; init; } = null!;
    public DbSet<Domain.Entities.Projects.Project> Projects { get; init; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HeroConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ProjectDetailsConfiguration).Assembly);

    }
}