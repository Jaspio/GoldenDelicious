using GoldenDelicious.DiscordBot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldenDelicious.DiscordBot.Data.Contexts;

public class DiscordBotDbContext(DbContextOptions<DiscordBotDbContext> options) : DbContext(options)
{
    public DbSet<ServerEntity> Servers => Set<ServerEntity>();
    public DbSet<GameActivityEntity> GameActivities => Set<GameActivityEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameActivityEntity>()
            .HasIndex(p => p.ServerId);

        modelBuilder.Entity<GameActivityEntity>()
            .HasIndex(p => p.GameName);

        modelBuilder.Entity<GameActivityEntity>()
            .HasIndex(p => p.UserId);
        
        modelBuilder.Entity<GameActivityEntity>()
            .HasIndex(p => p.TimestampStart);
        
        base.OnModelCreating(modelBuilder);
    }
}
