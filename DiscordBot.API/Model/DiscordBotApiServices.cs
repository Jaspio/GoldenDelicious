using GoldenDelicious.DiscordBot.Service;

namespace GoldenDelicious.DiscordBot.API.Model;

public class DiscordBotApiServices(
    DiscordBotService discordBotService,
    IOptions<DiscordBotApiOptions> options,
    ILogger<DiscordBotApiServices> logger
)
{
    public DiscordBotService DiscordBotService { get; } = discordBotService;
    public IOptions<DiscordBotApiOptions> Options { get; } = options;
    public ILogger<DiscordBotApiServices> Logger { get; } = logger;
}
