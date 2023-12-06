using Application.Abstractions.Messaging;

namespace Application.PlayerScores.GetById;

public sealed record GetPlayerScoreByIdQuery(int PlayerScoreId) : IQuery<PlayerScoreResponse>;
