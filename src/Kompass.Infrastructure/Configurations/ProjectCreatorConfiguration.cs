using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text.Json;

namespace Kompass.Infrastructure.Configurations;

public class ProjectCreatorConfiguration : IEntityTypeConfiguration<Domain.Entities.Projects.ProjectCreator>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Projects.ProjectCreator> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ProjectCreatorId.EfCoreValueConverter>();
    }
}