using Domain.Players;
using FluentValidation;

namespace Application.PlayerScores.Create;

public class CreatePlayerScoreCommandValidator : AbstractValidator<CreatePlayerScoreCommand>
{
    public CreatePlayerScoreCommandValidator(IPlayerRepository playerRepository)
    {
        RuleFor(c => c.PlayerId).MustAsync(async (playerId, _) =>
                await playerRepository.IsCurrentPlayerAsync(playerId))
            .WithMessage("You can't create score for other player");
    }
}
