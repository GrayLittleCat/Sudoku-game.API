using SharedKernel;

namespace Domain.PlayerScores;

public static class PlayerScoreErrors
{
    public static Error NotFound(int playerScoreId)
    {
        return Error.NotFound(
            "PlayerScores.NotFound", $"The player score with the Id = '{playerScoreId}' was not found");
    }

    public static Error NotFoundByPlayerId(int playerId)
    {
        return Error.NotFound(
            "PlayerScores.NotFoundByPlayerId", $"The player score with the Player Id = '{playerId}' was not found");
    }

    public static Error EmptyList()
    {
        return Error.Validation(
            "PlayerScores.EmptyList", "There are no player scores in system yet");
    }
}
