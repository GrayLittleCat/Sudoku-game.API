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
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

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
            return HandleFailure(response);
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
            return HandleFailure(response);
        }

        return Results.Ok(response.Value);
    }

    private static async Task<IResult> DeleteCommand(int playerId, ISender sender)
    {
        var command = new DeletePlayerCommand(playerId);
        var response = await sender.Send(command);
        if (response.IsFailure)
        {
            return HandleFailure(response);
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
            return HandleFailure(response);
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

    private static IResult HandleFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                TypedResults.BadRequest(
                    CreateProblemDetails(
                        "Validation Error", StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),
            _ =>
                TypedResults.BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };
    }

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null)
    {
        return new ProblemDetails
        {
            Title = title,
            Type = error.Code,
            Detail = error.Description,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
    }
}
