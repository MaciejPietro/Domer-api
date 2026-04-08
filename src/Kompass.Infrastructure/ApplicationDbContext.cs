using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Entities.Projects;
using Kompass.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kompass.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<Project> Projects { get; init; } = null!;
    public DbSet<Folder> Folders { get; init; } = null!;
    public DbSet<Document> Documents { get; init; } = null!;
    public DbSet<Device> Devices { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);
    }
}