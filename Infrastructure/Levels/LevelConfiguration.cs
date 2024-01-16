using Domain.Levels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Levels;

internal sealed class LevelConfiguration : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder.ToTable(TableNames.Levels);

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).ValueGeneratedNever();

        builder.Property(l => l.Name)
            .HasColumnType("VARCHAR2")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(l => l.Description)
            .HasColumnType("VARCHAR2")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.HasIndex(l => l.Name).IsUnique();

        builder.HasData(
            new Level(1, "Easy"),
            new Level(2, "Medium"),
            new Level(3, "Hard")
        );
    }
}
