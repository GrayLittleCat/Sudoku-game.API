using SharedKernel;

namespace Domain.PlayerScores;

public static class PlayerScoreErrors
{
    public static Error NotFound(int playerScoreId)
    {
        return new Error(
            "PlayerScores.NotFound", $"The player score with the Id = '{playerScoreId}' was not found");
    }

    public static Error NotFoundByPlayerId(int playerId)
    {
        return new Error(
            "PlayerScores.NotFoundByPlayerId", $"The player score with the Player Id = '{playerId}' was not found");
    }
}
