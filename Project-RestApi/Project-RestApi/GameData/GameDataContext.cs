using System;
using Microsoft.EntityFrameworkCore;
using Project_RestApi.Models;

namespace Project_RestApi.GameData;

public class GameDataContext : DbContext
{
    public GameDataContext(DbContextOptions<GameDataContext> options) : base(options) { }

    public DbSet<Player> Players { get; set; }
}

