using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PlayerScores;
using SharedKernel;

namespace Application.PlayerScores.Get;

internal sealed record GetPlayerScoresQueryHandler : IQueryHandler<GetPlayerScoresQuery, PagedList<PlayerScoreResponse>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetPlayerScoresQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<PagedList<PlayerScoreResponse>>> Handle(GetPlayerScoresQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = _dbConnectionFactory.CreateOpenConnection();

        const string sql =
            """
            SELECT ps.id,
                   ps.score,
                   ps.player_id AS PlayerId,
                   p.nickname AS PlayerName,
                   ps.level_id AS LevelId,
                   l.name AS LevelName
            FROM player_scores ps
            JOIN players p
              ON p.id = ps.player_id
            JOIN levels l
              ON l.id = ps.level_id
            ORDER BY ps.LEVEL_ID, ps.SCORE
            """;


        var playerScoreList = await PagedList<PlayerScoreResponse>.CreateAsync(
            sql,
            request.Page,
            request.PageSize,
            connection);


        if (playerScoreList.TotalCount == 0)
        {
            return Result.Failure<PagedList<PlayerScoreResponse>>(PlayerScoreErrors.EmptyList());
        }

        return playerScoreList;
    }
}
