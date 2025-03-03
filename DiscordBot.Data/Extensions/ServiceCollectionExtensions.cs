using GoldenDelicious.Common;
using GoldenDelicious.Common.Data;
using GoldenDelicious.DiscordBot.Data.Contexts;
using GoldenDelicious.DiscordBot.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoldenDelicious.DiscordBot.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IHostApplicationBuilder? AddPostgresDatabaseServices(this IHostApplicationBuilder? builder)
    {
        builder?.AddNpgsqlDbContext<DiscordBotDbContext>(
            ServiceNames.DatabaseName,
            configureDbContextOptions: dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseNpgsql(optionsBuilder =>
                {
                    optionsBuilder.EnableRetryOnFailure();
                });
            }
        );
        
        builder?.Services.AddScoped<IGameActivityRepository, GameActivityRepository>();
        builder?.Services.AddScoped<IServerRepository, ServerRepository>();

        return builder;
    }
}