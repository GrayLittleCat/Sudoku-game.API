using SharedKernel;

namespace Domain.Players;

public static class PlayerErrors
{
    public static Error NotFound(int playerId)
    {
        return Error.NotFound(
            "Players.NotFound", $"The player with the Id = '{playerId}' was not found");
    }

    public static Error NotFoundByEmail(string email)
    {
        return Error.NotFound(
            "Players.NotFoundByEmail", $"The player with the Email = '{email}' was not found");
    }
}
