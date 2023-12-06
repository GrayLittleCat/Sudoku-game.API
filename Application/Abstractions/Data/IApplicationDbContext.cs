using Domain.Levels;
using Domain.Players;
using Domain.PlayerScores;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Player> Players { get; set; }

    DbSet<PlayerScore> PlayerScores { get; set; }

    DbSet<Level> Levels { get; set; }
}
