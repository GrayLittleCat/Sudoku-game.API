using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.PlayerScores;
using SharedKernel;

namespace Application.PlayerScores.GetByPlayerId;

internal sealed record GetPlayerScoreByPlayerIdQueryHandler
    : IQueryHandler<GetPlayerScoreByPlayerIdQuery, PlayerScoreResponse>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetPlayerScoreByPlayerIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<PlayerScoreResponse>> Handle(GetPlayerScoreByPlayerIdQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = _dbConnectionFactory.CreateOpenConnection();

        const string sql =
            """
            SELECT ps.id,ps.player_id,p.nickname,ps.level_id,l.name
            FROM player_scores ps
            JOIN players p
              ON p.id = ps.player_id
            JOIN levels l
              ON l.id = ps.level_id
            WHERE ps.PLAYER_ID = @PlayerId
            """;

        PlayerScoreResponse? playerScore = await connection.QueryFirstOrDefaultAsync(
            sql,
            new { request.PlayerId });

        if (playerScore is null)
        {
            return Result.Failure<PlayerScoreResponse>(PlayerScoreErrors.NotFoundByPlayerId(request.PlayerId));
        }

        return playerScore;
    }
}
