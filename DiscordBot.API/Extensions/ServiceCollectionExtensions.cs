using GoldenDelicious.DiscordBot.Data.Extensions;

namespace GoldenDelicious.DiscordBot.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        /*//todo: https://learn.microsoft.com/en-us/dotnet/aspire/database/ef-core-migrations
        builder.Services.AddMigration<DiscordBotContext, DiscordBotContextSeed>();*/

        builder.AddRabbitMQClient("eventbus");
        //.AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>()
        ;

        builder.Services.AddOptions<DiscordBotApiOptions>()
            .BindConfiguration(nameof(DiscordBotApiOptions));
    }
}