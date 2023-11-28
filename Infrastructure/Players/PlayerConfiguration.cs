using Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Players;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(c => c.Id);

        builder.OwnsOne(player => player.Nickname, nickNameBuilder =>
        {
            nickNameBuilder.WithOwner();

            nickNameBuilder.Property(nickname => nickname.Value)
                .HasMaxLength(100)
                .IsRequired();
        });
        
        builder.OwnsOne(player => player.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(email => email.Value)
                .HasMaxLength(255)
                .IsRequired();
            
            emailBuilder.HasIndex(c => c.Value).IsUnique();
        });
        
        builder.HasIndex(c => c.IdentityId).IsUnique();
    }
}
