using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace GoldenDelicious.DiscordBot.Data.Models;

public class Server : DatabaseEntity
{
    /// <summary>
    /// ID of the Discord server/Guild
    /// </summary>
    public ulong GuildId { get; set; }

    /// <summary>
    /// Prefix the bot will respond to in the server/guild
    /// </summary>
    [StringLength(10)]
    [Required]
    public required string Prefix { get; set; }
    
    /// <summary>
    /// Channel to send logs to
    /// </summary>
    public ulong LoggingChannel { get; set; }

    /// <summary>
    /// Embed color
    /// </summary>
    [NotMapped]
    public Color EmbedColor { get; set; }
}