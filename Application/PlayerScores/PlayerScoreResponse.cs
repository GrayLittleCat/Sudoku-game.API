namespace Application.PlayerScores;

public sealed record PlayerScoreResponse
{
    public int Id { get; init; }
    public int PlayerId { get; init; }
    public string PlayerNickname { get; init; }
    public int LevelId { get; init; }
    public int LevelName { get; init; }
}
