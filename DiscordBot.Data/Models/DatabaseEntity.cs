using System.ComponentModel.DataAnnotations;

namespace GoldenDelicious.DiscordBot.Data.Models;

public abstract class DatabaseEntity
{
    // ID of the row in the database
    [Key]
    public ulong Id { get; set; }
}
