using Application.Abstractions.Messaging;

namespace Application.PlayerScores.Delete;

public record DeletePlayerScoreCommand(int PlayerScoreId) : ICommand;
