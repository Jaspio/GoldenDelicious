namespace GoldenDelicious.DiscordBot.Service;

public class DiscordBotOptions
{
    public required string BotToken { get; set; }
    public required char CommandPrefix { get; set; }
    public string? Game { get; set; }
}
