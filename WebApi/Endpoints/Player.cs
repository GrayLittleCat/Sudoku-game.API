using Application.Players.ChangePassword;
using Application.Players.Delete;
using Application.Players.GetById;
using Application.Players.Login;
using Application.Players.Register;
using Carter;
using Infrastructure.Authentication;
using MediatR;
using WebApi.Extensions;

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
        group.MapDelete("{playerId}", DeleteCommand).WithName(nameof(DeleteCommand))
            .RequireAuthorization(new HasPermissionAttribute(Permission.UpdateMember));
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
            return response.HandleFailure();
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
            return response.HandleFailure();
        }

        return Results.Ok(response.Value);
    }

    private static async Task<IResult> DeleteCommand(int playerId, ISender sender)
    {
        var command = new DeletePlayerCommand(playerId);
        var response = await sender.Send(command);
        if (response.IsFailure)
        {
            return response.HandleFailure();
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
            return response.HandleFailure();
        }

        return TypedResults.Ok();
    }

    private static async Task<IResult> GetPlayerById(
        int playerId, ISender sender)
    {
        var playerResponse = await sender.Send(new GetPlayerByIdQuery(playerId));

        if (playerResponse.IsFailure)
        {
            return playerResponse.HandleFailure();
        }

        return TypedResults.Ok(playerResponse.Value);
    }
}
