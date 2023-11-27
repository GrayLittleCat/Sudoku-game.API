using SharedKernel;

namespace Domain.Players;

public static class UserErrors
{
    public static Error NotFound(int playerId) => new(
        "Users.NotFound", $"The user with the Id = '{playerId}' was not found");
}
