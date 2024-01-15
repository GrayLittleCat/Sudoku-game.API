using Domain.PlayerRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.PlayerRoles;

public class PlayerRoleConfiguration : IEntityTypeConfiguration<PlayerRole>
{
    public void Configure(EntityTypeBuilder<PlayerRole> builder)
    {
        builder.HasKey(x => new { x.PlayerId, x.RoleId });
    }
}
