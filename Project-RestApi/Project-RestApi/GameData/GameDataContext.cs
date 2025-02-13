using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_RestApi.Models;

namespace Project_RestApi.GameData;

public class GameDataContext : IdentityDbContext<User>
{
    public GameDataContext(DbContextOptions dbContext) : base(dbContext) { }

    public DbSet<Player> Players { get; set; }
}