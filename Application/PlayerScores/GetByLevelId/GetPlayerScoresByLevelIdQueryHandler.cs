using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.PlayerScores;
using SharedKernel;

namespace Application.PlayerScores.GetByLevelId;

internal sealed record GetPlayerScoresByLevelIdQueryHandler
    : IQueryHandler<GetPlayerScoresByLevelIdQuery, PagedList<PlayerScoreResponse>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetPlayerScoresByLevelIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<PagedList<PlayerScoreResponse>>> Handle(GetPlayerScoresByLevelIdQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = _dbConnectionFactory.CreateOpenConnection();

        const string sql =
            @"
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
            WHERE ps.LEVEL_ID = :LevelId
            ";

        var param = new DynamicParameters();

        param.Add("LevelId", request.LevelId);

        var playerScoreList = await PagedList<PlayerScoreResponse>.CreateAsync(
            sql,
            request.Page,
            request.PageSize,
            connection,
            param);


        if (playerScoreList.TotalCount == 0)
        {
            return Result.Failure<PagedList<PlayerScoreResponse>>(PlayerScoreErrors.EmptyList());
        }

        return playerScoreList;
    }
}
