namespace GoldenDelicious.DiscordBot.Data.Models;

public abstract class DatabaseEntity
{
    // ID of the row in the database
    public ulong Id { get; set; }
}