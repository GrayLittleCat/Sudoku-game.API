using Application.Abstractions.Messaging;

namespace Application.PlayerScores.Create;

public sealed record CreatePlayerScoreCommand(int PlayerId, int LevelId, float Score) : ICommand;

public record CreatePlayerScoreRequest(int PlayerId, int LevelId, float Score);
