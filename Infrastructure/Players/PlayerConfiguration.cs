using Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Players;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable(TableNames.Players);

        builder.HasKey(p => p.Id);

        builder.OwnsOne(player => player.Nickname, nickNameBuilder =>
        {
            nickNameBuilder.WithOwner();

            nickNameBuilder.Property(nickname => nickname.Value)
                .HasColumnName(nameof(Player.Nickname).ToUpperInvariant())
                .HasColumnType("VARCHAR2")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(player => player.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(email => email.Value)
                .HasColumnName(nameof(Player.Email).ToUpperInvariant())
                .HasColumnType("VARCHAR2")
                .HasMaxLength(255)
                .IsRequired();

            emailBuilder.HasIndex(e => e.Value).IsUnique();
        });

        builder.Property(p => p.IdentityId)
            .HasColumnType("VARCHAR2")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(p => p.IdentityId).IsUnique();
    }
}
