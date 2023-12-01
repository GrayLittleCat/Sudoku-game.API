
using SharedKernel;

namespace Domain.Players;

public sealed class Player : Entity
{
    public Player(Email email, Name nickname, string identityId)
    {
        Email = email;
        Nickname = nickname;
        IdentityId = identityId;
    }
    
    //Needed for EF Core
    private Player()
    {
    }

    public Email Email { get; private set; }

    public Name Nickname { get; private set; }

    public string IdentityId { get; private set; }
}
