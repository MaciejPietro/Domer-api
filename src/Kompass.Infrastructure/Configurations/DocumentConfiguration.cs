using Kompass.Domain.Common;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Entities.Folders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kompass.Infrastructure.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<DocumentId.EfCoreValueConverter>();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Content)
            .IsRequired(false);

        builder.HasOne(f => f.Folder)
            .WithMany(f => f.Documents)
            .HasForeignKey(f => f.FolderId)
            .OnDelete(DeleteBehavior.Cascade) 
            .IsRequired(); 


        // No duplicate document names at the same level within a project
        builder.HasIndex(f => new { f.FolderId, f.Title })
            .IsUnique();
    }
}