﻿namespace Domain.Levels;

public sealed class Level
{
    public Level(int id, string levelName, string description)
    {
        Id = id;
        Name = levelName;
        Description = description;
    }

    public int Id { get; private set; }
    
    public string Name { get; private set; }
    
    public string Description { get; private set; }
}
