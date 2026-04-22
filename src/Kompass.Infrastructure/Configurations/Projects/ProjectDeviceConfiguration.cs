using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kompass.Infrastructure.Configurations.Projects;

public class ProjectDeviceConfiguration : IEntityTypeConfiguration<ProjectDevice>
{
    public void Configure(EntityTypeBuilder<ProjectDevice> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ProjectDeviceId.EfCoreValueConverter>();
        
        builder.Property(x => x.ProjectId)
            .HasConversion<ProjectId.EfCoreValueConverter>();

        builder.Property(x => x.DeviceId)
            .HasConversion<DeviceId.EfCoreValueConverter>();
        
        builder.HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey(x => x.DeviceId)
            .IsRequired();
    }
}