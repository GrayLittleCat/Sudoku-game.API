using Domain.Levels;
using Domain.Players;
using Domain.PlayerScores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.PlayerScores;

internal sealed class PlayerScoreConfiguration : IEntityTypeConfiguration<PlayerScore>
{
    public void Configure(EntityTypeBuilder<PlayerScore> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne<Player>()
            .WithMany()
            .HasForeignKey(ps => ps.PlayerId)
            .IsRequired();

        builder.HasOne<Level>()
            .WithMany()
            .HasForeignKey(ps => ps.LevelId)
            .IsRequired();

        builder.Property(ps => ps.Score)
            .HasColumnType("NUMBER")
            .IsRequired();

        builder.HasIndex(ps => new { ps.PlayerId, ps.LevelId }).IsUnique();
    }
}
