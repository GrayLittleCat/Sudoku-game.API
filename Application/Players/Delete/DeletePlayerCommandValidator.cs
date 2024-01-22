using Domain.Players;
using FluentValidation;

namespace Application.Players.Delete;

public class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
{
    public DeletePlayerCommandValidator(IPlayerRepository playerRepository)
    {
        RuleFor(c => c.PlayerId).MustAsync(async (playerId, _) =>
                await playerRepository.IsCurrentPlayerAsync(playerId))
            .WithMessage("You can't delete other player");
    }
}
