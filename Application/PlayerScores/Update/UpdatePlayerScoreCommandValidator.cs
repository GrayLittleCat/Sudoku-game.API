using Domain.Players;
using Domain.PlayerScores;
using FluentValidation;

namespace Application.PlayerScores.Update;

public class UpdatePlayerScoreCommandValidator : AbstractValidator<UpdatePlayerScoreCommand>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IPlayerScoreRepository _playerScoreRepository;

    public UpdatePlayerScoreCommandValidator(
        IPlayerRepository playerRepository,
        IPlayerScoreRepository playerScoreRepository)
    {
        _playerRepository = playerRepository;
        _playerScoreRepository = playerScoreRepository;
        RuleFor(c => c.Id).MustAsync(async (playerScoreId, _) =>
                await CanUpdatePlayerScoreAsync(playerScoreId))
            .WithMessage("You can't update score for other player");
    }

    private async Task<bool> CanUpdatePlayerScoreAsync(int playerScoreId)
    {
        var playerScore = await _playerScoreRepository.GetByIdAsync(playerScoreId);
        var currentPlayer = await _playerRepository.GetCurrentPlayerAsync();

        return playerScore?.PlayerId == currentPlayer?.Id;
    }
}
