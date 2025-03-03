using System.Drawing;

namespace GoldenDelicious.Common.Data;

public record DiscordServer(ulong GuildId, string Prefix, ulong LoggingChannel, Color EmbedColor);