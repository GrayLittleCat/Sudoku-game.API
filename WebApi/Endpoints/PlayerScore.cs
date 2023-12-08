using Application.PlayerScores.Create;
using Application.PlayerScores.Delete;
using Application.PlayerScores.Get;
using Application.PlayerScores.GetById;
using Application.PlayerScores.GetByPlayerId;
using Application.PlayerScores.Update;
using Carter;
using MediatR;

namespace WebApi.Endpoints;

public sealed class PlayerScore : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/player-scores")
            .RequireAuthorization();

        group.MapGet("", GetPlayerScores).WithName(nameof(GetPlayerScores));
        group.MapGet("{id}", GetPlayerScoreById).WithName(nameof(GetPlayerScoreById));
        group.MapGet("player/{playerId}", GetPlayerScoreByPlayerId).WithName(nameof(GetPlayerScoreByPlayerId));

        group.MapPost("", CreatePlayerScore);

        group.MapPut("{id}", UpdatePlayerScore).WithName(nameof(UpdatePlayerScore));

        group.MapDelete("{id}", DeletePlayerScore).WithName(nameof(DeletePlayerScore));
    }

    private static async Task<IResult> CreatePlayerScore(
        CreatePlayerScoreRequest request,
        ISender sender)
    {
        var command = new CreatePlayerScoreCommand(
            request.PlayerId,
            request.LevelId,
            request.Score);

        var response = await sender.Send(command);

        if (response.IsFailure)
        {
            return TypedResults.BadRequest(response.Error.Description);
        }

        return TypedResults.Ok();
    }

    private static async Task<IResult> UpdatePlayerScore(
        int id,
        UpdatePlayerScoreRequest request,
        ISender sender)
    {
        var command = new UpdatePlayerScore(id, request.Score);
        var response = await sender.Send(command);
        if (response.IsFailure)
        {
            return TypedResults.BadRequest(response.Error.Description);
        }

        return TypedResults.Ok();
    }

    private static async Task<IResult> DeletePlayerScore(int id, ISender sender)
    {
        var command = new DeletePlayerScoreCommand(id);
        var response = await sender.Send(command);
        if (response.IsFailure)
        {
            return TypedResults.BadRequest(response.Error.Description);
        }

        return TypedResults.Ok();
    }

    private static async Task<IResult> GetPlayerScoreById(int id, ISender sender)
    {
        var query = new GetPlayerScoreByIdQuery(id);

        var response = await sender.Send(query);

        if (response.IsFailure)
        {
            return TypedResults.NotFound(response.Error.Description);
        }

        return TypedResults.Ok(response.Value);
    }

    private static async Task<IResult> GetPlayerScoreByPlayerId(int playerId, ISender sender)
    {
        var query = new GetPlayerScoreByPlayerIdQuery(playerId);

        var response = await sender.Send(query);

        if (response.IsFailure)
        {
            return TypedResults.NotFound(response.Error.Description);
        }

        return TypedResults.Ok(response.Value);
    }

    private static async Task<IResult> GetPlayerScores(ISender sender)
    {
        var query = new GetPlayerScoresQuery();

        var response = await sender.Send(query);

        if (response.IsFailure)
        {
            return TypedResults.NotFound(response.Error.Description);
        }

        return TypedResults.Ok(response.Value);
    }
}
