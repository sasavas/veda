using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Veda.Application.Modules.RecipientModule.Models;

namespace Veda.Infrastructure.DataAccess.Configurations;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.HasMany(folder => folder.DigitalContents)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}