using Application.Abstractions.Messaging;

namespace Application.PlayerScores.GetByPlayerId;

public sealed record GetPlayerScoreByPlayerIdQuery(int PlayerId) : IQuery<PlayerScoreResponse>;
