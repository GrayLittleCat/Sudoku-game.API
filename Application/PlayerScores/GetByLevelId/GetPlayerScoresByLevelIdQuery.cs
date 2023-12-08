using Application.Abstractions.Messaging;

namespace Application.PlayerScores.GetByLevelId;

public sealed record GetPlayerScoresByLevelIdQuery(
    int LevelId,
    int Page,
    int PageSize) : IQuery<PagedList<PlayerScoreResponse>>;
