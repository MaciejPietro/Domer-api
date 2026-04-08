using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices.Camera;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kompass.Infrastructure.Configurations.Devices.Cameras;

public class CameraConfiguration : IEntityTypeConfiguration<Camera>
{
    public void Configure(EntityTypeBuilder<Camera> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<CameraId.EfCoreValueConverter>();

        builder.Property(x => x.DeviceId)
            .HasConversion<DeviceId.EfCoreValueConverter>()
            .IsRequired();

        builder.Property(x => x.HorizontalAngle)
            .IsRequired();

        builder.Property(x => x.VerticalAngle)
            .IsRequired();

        builder.Property(x => x.MaxDistance)
            .IsRequired();

        // 1:1 relationship with Device
        builder.HasOne(c => c.Device)
            .WithOne()
            .HasForeignKey<Camera>(c => c.DeviceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
