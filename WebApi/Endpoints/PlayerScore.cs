using Application.PlayerScores.Create;
using Carter;
using MediatR;

namespace WebApi.Endpoints;

public sealed class PlayerScore : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/player-score");

        group.MapPost("", CreatePlayerScoreCommand).RequireAuthorization();
    }

    private static async Task<IResult> CreatePlayerScoreCommand(
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
}
