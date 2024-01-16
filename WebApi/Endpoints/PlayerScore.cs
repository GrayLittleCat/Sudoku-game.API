using Application.PlayerScores.Create;
using Application.PlayerScores.Delete;
using Application.PlayerScores.Get;
using Application.PlayerScores.GetById;
using Application.PlayerScores.GetByLevelId;
using Application.PlayerScores.GetByPlayerId;
using Application.PlayerScores.Update;
using Carter;
using Infrastructure.Authentication;
using MediatR;

namespace WebApi.Endpoints;

public sealed class PlayerScore : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/player-scores")
            .RequireAuthorization(new HasPermissionAttribute(Permission.ReadMember));

        group.MapGet("", GetPlayerScores).WithName(nameof(GetPlayerScores));
        group.MapGet("{id}", GetPlayerScoreById).WithName(nameof(GetPlayerScoreById));
        group.MapGet("player/{playerId}", GetPlayerScoreByPlayerId).WithName(nameof(GetPlayerScoreByPlayerId));
        group.MapGet("level/{levelId}", GetPlayerScoreByLevelId).WithName(nameof(GetPlayerScoreByLevelId));

        group.MapPost("", CreatePlayerScore).WithName(nameof(CreatePlayerScore))
            .RequireAuthorization(new HasPermissionAttribute(Permission.UpdateMember));

        group.MapPut("{id}", UpdatePlayerScore).WithName(nameof(UpdatePlayerScore))
            .RequireAuthorization(new HasPermissionAttribute(Permission.UpdateMember));

        group.MapDelete("{id}", DeletePlayerScore).WithName(nameof(DeletePlayerScore))
            .RequireAuthorization(new HasPermissionAttribute(Permission.UpdateMember));
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

    private static async Task<IResult> GetPlayerScoreByLevelId(int levelId, int page, int pageSize, ISender sender)
    {
        var query = new GetPlayerScoresByLevelIdQuery(levelId, page, pageSize);

        var response = await sender.Send(query);

        if (response.IsFailure)
        {
            return TypedResults.NotFound(response.Error.Description);
        }

        return TypedResults.Ok(response.Value);
    }

    private static async Task<IResult> GetPlayerScores(
        int page,
        int pageSize,
        ISender sender)
    {
        var query = new GetPlayerScoresQuery(page, pageSize);

        var response = await sender.Send(query);

        if (response.IsFailure)
        {
            return TypedResults.NotFound(response.Error.Description);
        }

        return TypedResults.Ok(response.Value);
    }
}
