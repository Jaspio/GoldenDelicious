using GoldenDelicious.DiscordBot.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoldenDelicious.DiscordBot.Data.Contexts;

public class DiscordBotDbContext(DbContextOptions<DiscordBotDbContext> options) : DbContext(options)
{
    public DbSet<Server> Servers => Set<Server>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DefineServer(modelBuilder.Entity<Server>());
    }

    private static void DefineServer(EntityTypeBuilder<Server> entityBuilder)
    {
        entityBuilder.ToTable("Server");
        entityBuilder.HasKey(s => s.Id);
        entityBuilder.Property(s => s.Prefix).HasMaxLength(10).IsRequired();
        entityBuilder.Property(s => s.GuildId);
        entityBuilder.Property(s => s.LoggingChannel);
        entityBuilder.Ignore(s => s.EmbedColor);
    }
}
