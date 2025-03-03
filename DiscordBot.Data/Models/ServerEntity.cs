using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using GoldenDelicious.Common.Data;

namespace GoldenDelicious.DiscordBot.Data.Models;

public class ServerEntity : DatabaseEntity
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

    public static explicit operator ServerEntity(DiscordServer server)
    {
        return new ServerEntity
        { 
            GuildId = server.GuildId,
            Prefix = server.Prefix,
            LoggingChannel = server.LoggingChannel,
            EmbedColor = server.EmbedColor
        };
    }
    
    public static explicit operator DiscordServer(ServerEntity server)
    {
        return new DiscordServer(
            server.GuildId, 
            server.Prefix, 
            server.LoggingChannel, 
            server.EmbedColor);
    }
}