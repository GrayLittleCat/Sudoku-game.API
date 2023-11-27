using Domain.Levels;

namespace Domain.PlayerScores;

public interface IPlayerScoreRepository
{
    Task<Level?> GetByIdAsync(int id);

    void Add(PlayerScore playerScore);

    void Update(PlayerScore playerScore);
}
