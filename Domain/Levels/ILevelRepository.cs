namespace Domain.Levels;

public interface ILevelRepository
{
    Task<Level?> GetByIdAsync(int id);

    void Insert(Level level);

    void Update(Level level);

    void Delete(Level level);
}
