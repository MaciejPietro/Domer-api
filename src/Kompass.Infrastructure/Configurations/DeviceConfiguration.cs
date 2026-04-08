using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Entities.Folders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kompass.Infrastructure.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<DeviceId.EfCoreValueConverter>();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(1000);

        // Index on Type for filtering
        builder.HasIndex(x => x.Type);

        // Index on CreatedAt for sorting
        builder.HasIndex(x => x.CreatedAt);
    }
}