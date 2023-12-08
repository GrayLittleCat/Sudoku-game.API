using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.PlayerScores;
using SharedKernel;

namespace Application.PlayerScores.Get;

internal sealed record GetPlayerScoresQueryHandler : IQueryHandler<GetPlayerScoresQuery, List<PlayerScoreResponse>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetPlayerScoresQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<List<PlayerScoreResponse>>> Handle(GetPlayerScoresQuery request,
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
            ";


        var playerScores = await connection.QueryAsync<PlayerScoreResponse>(sql);
        var playerScoreList = playerScores.ToList();


        if (playerScoreList.Count == 0)
        {
            return Result.Failure<List<PlayerScoreResponse>>(PlayerScoreErrors.EmptyList());
        }

        return playerScoreList;
    }
}
