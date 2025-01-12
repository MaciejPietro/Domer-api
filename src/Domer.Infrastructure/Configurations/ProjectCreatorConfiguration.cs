using Domer.Domain.Common;
using Domer.Domain.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text.Json;

namespace Domer.Infrastructure.Configurations;

public class ProjectCreatorConfiguration : IEntityTypeConfiguration<Domain.Entities.Projects.ProjectCreator>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Projects.ProjectCreator> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ProjectCreatorId.EfCoreValueConverter>();

        
        // Configure one-to-one relationship
        builder.HasOne(pd => pd.Project)
            .WithOne(p => p.ProjectCreator)
            .HasForeignKey<ProjectCreator>(pd => pd.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}