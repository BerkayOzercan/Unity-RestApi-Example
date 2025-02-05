using Microsoft.EntityFrameworkCore;
using Project_RestApi.Models;

namespace Project_RestApi.GameData;

public class GameDataContext : DbContext
{
    public GameDataContext(DbContextOptions dbContext) : base(dbContext) { }

    public DbSet<Player> Players { get; set; }
}


