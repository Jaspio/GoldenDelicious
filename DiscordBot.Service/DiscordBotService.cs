using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Options;

namespace GoldenDelicious.DiscordBot.Service;

internal class DiscordBotService(
    ILogger<DiscordBotService> logger,
    DiscordSocketClient client,
    CommandService commandService,
    IOptions<DiscordBotOptions> settings,
    CancellationTokenSource tokenSource
) : IHostedService
{
    private readonly ILogger _logger = logger;
    private readonly DiscordBotOptions _settings = settings.Value;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        client.Log += Log;
        commandService.Log += Log;
        client.MessageReceived += HandleCommandAsync;

        // Register the command modules
        await commandService.AddModulesAsync(typeof(DiscordBotService).Assembly, null);

        // Login and connect.
        await client.LoginAsync(TokenType.Bot, _settings.BotToken);
        await client.StartAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await tokenSource.CancelAsync();
        await client.StopAsync();
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
            || message.HasMentionPrefix(client.CurrentUser, ref argPos);

        if (!isCommand || message.Author.IsBot)
            return;

        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(client, message);

        // Execute the command with the command context we just created, along with the service provider for precondition checks
        await commandService.ExecuteAsync(context, argPos, null);
    }
}
