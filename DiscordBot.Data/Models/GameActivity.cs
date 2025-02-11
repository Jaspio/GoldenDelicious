using System;

namespace DiscordBot.Data.Models;

public class GameActivity
{
    public int Id { get; set; }

    public ulong ServerId { get; set; }

    public ulong UserId { get; set; }

    public required string UserName { get; set; }

    public required string UserDisplayName { get; set; }

    public required string GameName { get; set; }
    public DateTime Timestamp { get; set; }
}
