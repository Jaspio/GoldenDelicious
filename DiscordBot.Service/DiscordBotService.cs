using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Options;

namespace GoldenDelicious.DiscordBot.Service;

public class DiscordBotService(
    ILogger<DiscordBotService> logger,
    DiscordSocketClient discordClient,
    CommandService commandService,
    IOptions<DiscordBotOptions> settings,
    CancellationTokenSource tokenSource,
    IServiceProvider serviceProvider
) : IHostedService
{
    private readonly ILogger _logger = logger;
    private readonly DiscordBotOptions _settings = settings.Value;
    
    public readonly DiscordSocketClient DiscordClient = discordClient;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        DiscordClient.Log += Log;
        commandService.Log += Log;
        DiscordClient.MessageReceived += HandleCommandAsync;

        // Register the command modules
        await commandService.AddModulesAsync(typeof(DiscordBotService).Assembly, serviceProvider);

        // Login and connect.
        await DiscordClient.LoginAsync(TokenType.Bot, _settings.BotToken);
        await DiscordClient.StartAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await tokenSource.CancelAsync();
        await DiscordClient.StopAsync();
        _logger.LogInformation("Shutdown complete");
    }

    private Task Log(LogMessage msg)
    {
        _logger.Log(msg.Severity.ToLogLevel(), $"{msg.Source}: {msg.Message}");
        return Task.CompletedTask;
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        // Don't process the command if it was a system message
        if (messageParam is not SocketUserMessage message)
            return;

        // Create a number to track where the prefix ends and the command begins
        int argPos = 0;

        // Determine if the message is a command based on the prefix and make sure no bots trigger commands
        bool isCommand =
            message.HasCharPrefix(_settings.CommandPrefix, ref argPos)
            || message.HasMentionPrefix(DiscordClient.CurrentUser, ref argPos);

        if (!isCommand || message.Author.IsBot)
            return;

        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(DiscordClient, message);

        // Execute the command with the command context we just created, along with the service provider for precondition checks
        await commandService.ExecuteAsync(context, argPos, null);
    }
}
