using SharedKernel;

namespace Domain.Levels;

public sealed class Level : Entity
{
    public Level(int id, string levelName, string? description = null)
    {
        Id = id;
        Name = levelName;
        Description = description;
    }

    //Needed for EF Core
    public Level()
    {
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }
}
