using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.PlayerScores;
using SharedKernel;

namespace Application.PlayerScores.GetById;

internal sealed record GetPlayerScoreByIdQueryHandler : IQueryHandler<GetPlayerScoreByIdQuery, PlayerScoreResponse>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetPlayerScoreByIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<PlayerScoreResponse>> Handle(GetPlayerScoreByIdQuery request,
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
            WHERE ps.ID = @PlayerScoreId
            """;

        PlayerScoreResponse? playerScore = await connection.QueryFirstOrDefaultAsync(
            sql,
            new { request.PlayerScoreId });
        if (playerScore is null)
        {
            return Result.Failure<PlayerScoreResponse>(PlayerScoreErrors.NotFound(request.PlayerScoreId));
        }

        return playerScore;
    }
}
