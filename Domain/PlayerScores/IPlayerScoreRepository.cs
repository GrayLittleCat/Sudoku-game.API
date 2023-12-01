namespace Domain.PlayerScores;

public interface IPlayerScoreRepository
{
    Task<PlayerScore?> GetByIdAsync(int id);

    void Insert(PlayerScore playerScore);

    void Update(PlayerScore playerScore);

    void Delete(PlayerScore playerScore);
}
