using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_RestApi.Models;

namespace Project_RestApi.GameData;

public class GameDataContext : IdentityDbContext<User>
{
    public GameDataContext(DbContextOptions dbContext) : base(dbContext) { }

    public DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1d53b4f6-5c91-4f23-8f7c-8c6c965d63aa", // Static Id
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "4e3a91b0-3b1f-4e56-a749-a2992e31452e", // Static Id  
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}