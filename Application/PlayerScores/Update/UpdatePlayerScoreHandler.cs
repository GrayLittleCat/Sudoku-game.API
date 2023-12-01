using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PlayerScores;
using SharedKernel;

namespace Application.PlayerScores.Update;

internal sealed class UpdatePlayerScoreHandler : ICommandHandler<UpdatePlayerScore>
{
    private readonly IPlayerScoreRepository _playerScoreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePlayerScoreHandler(IUnitOfWork unitOfWork, IPlayerScoreRepository playerScoreRepository)
    {
        _unitOfWork = unitOfWork;
        _playerScoreRepository = playerScoreRepository;
    }

    public async Task<Result> Handle(UpdatePlayerScore request, CancellationToken cancellationToken)
    {
        var playerScore = await _playerScoreRepository.GetByIdAsync(request.Id);

        if (playerScore is null)
        {
            return Result.Failure(new Error("PlayerScore.NotFound", $"Player with id:[{request.Id}] not found"));
        }

        playerScore.Update(request.Score);

        _playerScoreRepository.Update(playerScore);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
