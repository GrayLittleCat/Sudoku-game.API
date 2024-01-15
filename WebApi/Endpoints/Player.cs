using Application.Players;
using Application.Players.ChangePassword;
using Application.Players.Delete;
using Application.Players.GetById;
using Application.Players.Login;
using Application.Players.Register;
using Carter;
using Domain.Players;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.Endpoints;

public sealed class Player : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("register", RegisterPlayerCommand);

        app.MapPost("login", LoginCommand);

        var group = app.MapGroup("api/player")
            .RequireAuthorization(new HasPermissionAttribute(Permission.ReadMember));

        group.MapGet("{playerId}", GetPlayerById).WithName(nameof(GetPlayerById));
        group.MapPut("{playerId}/change-password", ChangePasswordCommand).WithName(nameof(ChangePasswordCommand));
        group.MapDelete("{playerId}", DeleteCommand).WithName(nameof(DeleteCommand));
    }

    private static async Task<IResult> RegisterPlayerCommand(
        RegisterPlayerRequest request,
        ISender sender)
    {
        var command = new RegisterPlayerCommand(
            request.Email,
            request.Password,
            request.Name);

        var response = await sender.Send(command);

        if (response.IsFailure)
        {
            return TypedResults.BadRequest(response.Error.Description);
        }

        return TypedResults.Ok(response.Value);
    }

    private static async Task<IResult> LoginCommand(
        LoginCommand command,
        ISender sender)
    {
        var response = await sender.Send(command);

        if (response.IsFailure)
        {
            return TypedResults.BadRequest(response.Error.Description);
        }

        return Results.Ok(response.Value);
    }

    private static async Task<IResult> DeleteCommand(int playerId, ISender sender)
    {
        var command = new DeletePlayerCommand(playerId);
        var response = await sender.Send(command);
        if (response.IsFailure)
        {
            return TypedResults.BadRequest(response.Error.Description);
        }

        return TypedResults.Ok();
    }

    private static async Task<IResult> ChangePasswordCommand(
        int playerId,
        ChangePasswordRequest request,
        ISender sender)
    {
        var command = new ChangePasswordCommand(playerId, request.NewPassword);
        var response = await sender.Send(command);
        if (response.IsFailure)
        {
            return TypedResults.BadRequest(response.Error.Description);
        }

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok<PlayerResponse>, NotFound<string>, BadRequest<string>>> GetPlayerById(
        int playerId, ISender sender)
    {
        var playerResponse = await sender.Send(new GetPlayerByIdQuery(playerId));

        if (playerResponse.IsFailure)
        {
            if (playerResponse.Error == PlayerErrors.NotFound(playerId))
            {
                return TypedResults.NotFound(playerResponse.Error.Description);
            }

            return TypedResults.BadRequest(playerResponse.Error.Description);
        }

        return TypedResults.Ok(playerResponse.Value);
    }
}
