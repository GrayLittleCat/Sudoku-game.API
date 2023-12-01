using Domain.Levels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Levels;

internal sealed class LevelConfiguration : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name)
            .HasColumnType("VARCHAR2")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(l => l.Description)
            .HasColumnType("VARCHAR2")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.HasIndex(l => l.Name).IsUnique();
    }
}
