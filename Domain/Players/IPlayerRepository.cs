namespace Domain.Players;

public interface IPlayerRepository
{
    Task<Player?> GetByIdAsync(int id);

    Task<bool> IsEmailUniqueAsync(string email);

    void Add(Player player);
}
