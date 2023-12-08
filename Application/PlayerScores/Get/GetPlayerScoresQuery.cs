using Application.Abstractions.Messaging;

namespace Application.PlayerScores.Get;

public sealed record GetPlayerScoresQuery : IQuery<List<PlayerScoreResponse>>;
