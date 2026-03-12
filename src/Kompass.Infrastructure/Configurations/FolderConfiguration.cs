using Kompass.Domain.Common;
using Kompass.Domain.Entities.Folders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kompass.Infrastructure.Configurations;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<FolderId.EfCoreValueConverter>();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);
        

        // Self-referencing relationship
        builder.HasOne(f => f.ParentFolder)
            .WithMany(f => f.SubFolders)
            .HasForeignKey(f => f.ParentFolderId)
            .OnDelete(DeleteBehavior.Restrict) // intentional — no cascade on self-ref
            .IsRequired(false); // nullable = root folder

        // Belongs to a project
        builder.HasOne(f => f.Project)
            .WithMany(p => p.Folders)
            .HasForeignKey(f => f.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // No duplicate folder names at the same level within a project
        builder.HasIndex(f => new { f.ProjectId, f.ParentFolderId, f.Name })
            .IsUnique();
    }
}
