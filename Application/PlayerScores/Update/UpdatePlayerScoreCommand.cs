using Application.Abstractions.Messaging;

namespace Application.PlayerScores.Update;

public sealed record UpdatePlayerScoreCommand(int Id, float Score) : ICommand;

public record UpdatePlayerScoreRequest(float Score);
