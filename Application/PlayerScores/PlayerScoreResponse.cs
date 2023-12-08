namespace Application.PlayerScores;

public sealed record PlayerScoreResponse
{
    public int Id { get; init; }
    public float Score { get; init; }
    public int PlayerId { get; init; }
    public string PlayerName { get; init; }
    public int LevelId { get; init; }
    public string LevelName { get; init; }
}
