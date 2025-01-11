using Domer.Domain.Common;
using Domer.Domain.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text.Json;

namespace Domer.Infrastructure.Configurations;

public class ProjectDetailsConfiguration : IEntityTypeConfiguration<Domain.Entities.Projects.ProjectDetails>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Projects.ProjectDetails> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ProjectDetailsId.EfCoreValueConverter>();

        
        builder.Property(x => x.Urls)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<ExternalUrl>>(v, (JsonSerializerOptions)null));

        // Configure one-to-one relationship
        builder.HasOne(pd => pd.Project)
            .WithOne(p => p.ProjectDetails)
            .HasForeignKey<ProjectDetails>(pd => pd.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}