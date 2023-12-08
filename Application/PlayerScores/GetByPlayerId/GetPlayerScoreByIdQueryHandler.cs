using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.PlayerScores;
using SharedKernel;

namespace Application.PlayerScores.GetByPlayerId;

internal sealed record GetPlayerScoreByPlayerIdQueryHandler
    : IQueryHandler<GetPlayerScoreByPlayerIdQuery, List<PlayerScoreResponse>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetPlayerScoreByPlayerIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<List<PlayerScoreResponse>>> Handle(GetPlayerScoreByPlayerIdQuery request,
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
            WHERE ps.PLAYER_ID = :PlayerId
            """;

        var playerScore = await connection.QueryAsync<PlayerScoreResponse>(
            sql,
            new { request.PlayerId });

        var playerScoreResponses = playerScore.ToList();
        if (playerScoreResponses.Count == 0)
        {
            return Result.Failure<List<PlayerScoreResponse>>(PlayerScoreErrors.NotFoundByPlayerId(request.PlayerId));
        }

        return playerScoreResponses;
    }
}
