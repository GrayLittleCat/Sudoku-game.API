using SharedKernel;

namespace Domain.Players;

public sealed class PlayerErrors
{
    public static Error NotFound(int playerId)
    {
        return new Error(
            "Players.NotFound", $"The player with the Id = '{playerId}' was not found");
    }

    public static Error NotFoundByEmail(string email)
    {
        return new Error(
            "Players.NotFoundByEmail", $"The player with the Email = '{email}' was not found");
    }
}
