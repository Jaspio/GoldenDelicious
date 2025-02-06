using GoldenDelicious.DiscordBot.Data.Contexts;

namespace GoldenDelicious.DiscordBot.API.Model;

public class DiscordBotApiServices(
    DiscordBotDbContext context,
    IOptions<DiscordBotApiOptions> options,
    ILogger<DiscordBotApiServices> logger)
{
    public DiscordBotDbContext Context { get; } = context;
    public IOptions<DiscordBotApiOptions> Options { get; } = options;
    public ILogger<DiscordBotApiServices> Logger { get; } = logger;
}