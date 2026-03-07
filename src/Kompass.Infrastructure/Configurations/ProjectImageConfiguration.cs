using Kompass.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kompass.Infrastructure.Configurations;

public class ProjectImageConfiguration : IEntityTypeConfiguration<Domain.Entities.Projects.ProjectImage>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Projects.ProjectImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ProjectImageId.EfCoreValueConverter>();

    }
}