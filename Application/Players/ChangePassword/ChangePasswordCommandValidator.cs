using Domain.Players;
using FluentValidation;

namespace Application.Players.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(IPlayerRepository playerRepository)
    {
        RuleFor(c => c.PlayerId).MustAsync(async (playerId, _) =>
                await playerRepository.IsCurrentPlayerAsync(playerId))
            .WithMessage("You can't change other player password");
    }
}
