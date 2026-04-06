using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kompass.Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Domain.Entities.Projects.Project>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Projects.Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ProjectId.EfCoreValueConverter>();
        
        builder.HasOne(p => p.ProjectDetails)
            .WithOne()
            .HasForeignKey<ProjectDetails>(pd => pd.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(p => p.ProjectCreator)
            .WithOne()
            .HasForeignKey<ProjectCreator>(pc => pc.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}