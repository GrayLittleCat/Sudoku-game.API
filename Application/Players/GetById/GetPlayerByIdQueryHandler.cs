using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Players;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Players.GetById;

internal sealed record GetPlayerByIdQueryHandler : IQueryHandler<GetPlayerByIdQuery, PlayerResponse>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetPlayerByIdQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<PlayerResponse>> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
    {
        var player = await _applicationDbContext.Players
            .AsNoTracking()
            .Where(p => p.Id == query.PlayerId)
            .Select(p => new PlayerResponse
            {
                Id = p.Id,
                Name = p.Nickname.Value,
                Email = p.Email.Value,
                IdentityId = p.IdentityId
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (player is null)
        {
            return Result.Failure<PlayerResponse>(PlayerErrors.NotFound(query.PlayerId));
        }

        return player;
    }
}
