using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GoldenDelicious.Common.Data;
using GoldenDelicious.DiscordBot.Data.Repositories;

namespace GoldenDelicious.DiscordBot.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient<IGameActivityRepository, GameActivityRepository>();
        
        builder.Services.AddHostedService<DiscordBotService>();
        builder.Services.AddSingleton<CancellationTokenSource>();

        builder.AddDiscordServices();
    }

    private static void AddDiscordServices(this IHostApplicationBuilder builder)
    {
        builder
            .Services.AddOptions<DiscordBotOptions>()
            .BindConfiguration(nameof(DiscordBotOptions));

        builder.Services.AddSingleton(
            new DiscordSocketConfig()
            {
                AlwaysDownloadUsers = true,
                LogLevel = Discord.LogSeverity.Verbose,
                MessageCacheSize = 1000,
                GatewayIntents = GatewayIntents.All,
            }
        );

        builder.Services.AddSingleton<DiscordSocketClient>();
        builder.Services.AddSingleton<CommandService>();

        builder.Services.Scan(scan => scan
            .FromAssemblyOf<DiscordBotService>()
            .AddClasses(classes => classes.AssignableTo<ModuleBase>())
            .AsSelf()
            .WithScopedLifetime()
        );
    }
}
