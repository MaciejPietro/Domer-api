using Domer.Domain.Common;
using Domer.Domain.Entities.Projects;
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
        
        builder.HasOne(p => p.ProjectDetails)
            .WithOne(pd => pd.Project)
            .HasForeignKey<ProjectDetails>(pd => pd.ProjectId);
    }
}