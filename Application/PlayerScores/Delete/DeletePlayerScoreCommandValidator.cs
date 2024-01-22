using Domain.Players;
using Domain.PlayerScores;
using FluentValidation;

namespace Application.PlayerScores.Delete;

public class DeletePlayerScoreCommandValidator : AbstractValidator<DeletePlayerScoreCommand>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IPlayerScoreRepository _playerScoreRepository;

    public DeletePlayerScoreCommandValidator(
        IPlayerRepository playerRepository,
        IPlayerScoreRepository playerScoreRepository)
    {
        _playerRepository = playerRepository;
        _playerScoreRepository = playerScoreRepository;
        RuleFor(c => c.PlayerScoreId).MustAsync(async (playerScoreId, _) =>
                await CanDeletePlayerScoreAsync(playerScoreId))
            .WithMessage("You can't delete score for other player");
    }

    private async Task<bool> CanDeletePlayerScoreAsync(int playerScoreId)
    {
        var playerScore = await _playerScoreRepository.GetByIdAsync(playerScoreId);
        var currentPlayer = await _playerRepository.GetCurrentPlayerAsync();

        return playerScore?.PlayerId == currentPlayer?.Id;
    }
}
