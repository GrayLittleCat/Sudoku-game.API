using Domain.Permissions;
using Domain.Players;
using SharedKernel;

namespace Domain.Roles;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Registered = new(1, "Registered");

    public Role(int id, string name)
        : base(id, name)
    {
    }

    public ICollection<Permission> Permissions { get; set; }
    public ICollection<Player> Players { get; set; }
}
