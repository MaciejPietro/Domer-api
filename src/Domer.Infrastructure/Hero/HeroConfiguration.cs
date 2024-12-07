
using Domer.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domer.Infrastructure.Hero;

public class HeroConfiguration : IEntityTypeConfiguration<Domain.Heroes.Entities.Hero>
{
    public void Configure(EntityTypeBuilder<Domain.Heroes.Entities.Hero> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<HeroId.EfCoreValueConverter>();
    }
}