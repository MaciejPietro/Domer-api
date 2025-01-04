using Domer.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domer.Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Domain.Entities.Projects.Project>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Projects.Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ProjectId.EfCoreValueConverter>();
    }
}