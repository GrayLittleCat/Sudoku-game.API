using Application.Abstractions.Messaging;

namespace Application.PlayerScores.Update;

public sealed record UpdatePlayerScore(int Id, float Score) : ICommand;

public record UpdatePlayerScoreRequest(float Score);
