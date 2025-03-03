using GoldenDelicious.DiscordBot.DatabaseMigration;
using GoldenDelicious.DiscordBot.Data.Extensions;
using GoldenDelicious.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<MigrationWorker>();

// GoldenDelicious.DiscordBot.Data.Extensions
builder.AddPostgresDatabaseServices();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(MigrationWorker.ActivityName));

var host = builder.Build();

host.Run();