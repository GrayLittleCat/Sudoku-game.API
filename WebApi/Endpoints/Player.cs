using Application.Players.GetById;
using Application.Players.Login;
using Application.Players.Register;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace WebApi.Endpoints;

public sealed class Player : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/players");
        group.MapPost("", RegisterPlayerCommand);

        app.MapPost("login", LoginCommand);

        group.MapGet("{id}", GetPlayerById).WithName(nameof(GetPlayerById));
    }

    private static async Task<IResult> RegisterPlayerCommand(
        RegisterPlayerRequest request,
        ISender sender)
    {
        var command = new RegisterPlayerCommand(
            request.Email,
            request.Password,
            request.Name);

        await sender.Send(command);

        return TypedResults.Ok();
    }

    private static async Task<IResult> LoginCommand(
        LoginCommand command,
        ISender sender)
    {
        return Results.Ok(await sender.Send(command));
    }

    private static async Task<IResult> GetPlayerById(int playerId, ISender sender)
    {
        var playerResponse = await sender.Send(new GetPlayerByIdQuery(playerId));

        return Results.Ok(playerResponse);
    }
}
