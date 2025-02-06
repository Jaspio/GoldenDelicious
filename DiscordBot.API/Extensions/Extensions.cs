using GoldenDelicious.DiscordBot.Data.Contexts;

namespace GoldenDelicious.DiscordBot.API.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<DiscordBotDbContext>("discordbotdb", configureDbContextOptions: dbContextOptionsBuilder =>
        {
            dbContextOptionsBuilder.UseNpgsql(contextOptionsBuilder =>
            {
                contextOptionsBuilder.UseVector();
            });
        });

        /*//todo: https://learn.microsoft.com/en-us/dotnet/aspire/database/ef-core-migrations
        builder.Services.AddMigration<DiscordBotContext, DiscordBotContextSeed>();*/
        
        builder.AddRabbitMQClient("eventbus");
            //.AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>()
            ;
        
        builder.Services.AddOptions<DiscordBotApiOptions>()
            .BindConfiguration(nameof(DiscordBotApiOptions));
    }
}