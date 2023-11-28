using Application.Players.Register;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.Endpoints;

public sealed class Player : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/players");
        group.MapPost("", RegisterPlayerCommand);
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
}
