
using SharedKernel;

namespace Domain.Players;

public sealed class Player : Entity
{
    public Player(int id, Email email, Name nickname, string identityId)
    :base (default)
    {
        Email = email;
        Nickname = nickname;
        IdentityId = identityId;
    }

    public Email Email { get; private set; }

    public Name Nickname { get; private set; }

    public string IdentityId { get; private set; }
}
