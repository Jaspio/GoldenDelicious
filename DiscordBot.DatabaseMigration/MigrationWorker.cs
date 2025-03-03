using System.Diagnostics;
using GoldenDelicious.DiscordBot.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

namespace GoldenDelicious.DiscordBot.DatabaseMigration;

public class MigrationWorker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    ILogger<MigrationWorker> logger) : BackgroundService
{

    internal const string ActivityName = "MigrationService";
    private static readonly ActivitySource _activitySource = new(ActivityName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = _activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DiscordBotDbContext>();

            await dbContext.Database.MigrateAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            logger.LogError("Exception during migration at {timestamp}", DateTimeOffset.UtcNow);
            activity?.RecordException(ex);
            throw;
        }
        
        hostApplicationLifetime.StopApplication();
    }
}