namespace Domain.Levels;

public interface ILevelRepository
{
    Task<Level?> GetByIdAsync(int id);

    void Add(Level level);

    void Update(Level level);

    void Remove(Level level);
}
