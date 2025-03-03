using GoldenDelicious.AppHost;
using GoldenDelicious.Common;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();

var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "https";

// Add Aspire hosting components.

#region Redis Cache

var redis = builder
    .AddRedis("redis")
    .WithRedisCommander();

#endregion

#region RabbitMQ

var rabbitMq = builder
    .AddRabbitMQ("eventbus");

#endregion

#region Database

var postgresPassword = builder.AddParameter("postgres-password", secret: true);

var botData = builder
    .AddPostgres(ServiceNames.DatabaseServer, postgresPassword)
    .WithImage("ankane/pgvector")
    .WithImageTag("latest")
    .AddDatabase(ServiceNames.DatabaseName);

var migrationService = builder.AddProject<DiscordBot_DatabaseMigration>(ServiceNames.DatabaseMigration)
    .WithReference(botData);

#endregion

// Add the distributed applications

#region DiscordBot_Service

var botService = builder
    .AddProject<Projects.DiscordBot_Service>(ServiceNames.DiscordBot)
    .WithReference(botData);

#endregion

#region DiscordBot_API

var botApi = builder
    .AddProject<Projects.DiscordBot_API>(ServiceNames.DiscordBotApi)
    .WithReference(botService)
    .WithReference(botData);

#endregion

// Complete the builder configuration.
await builder.Build().RunAsync();

// For test use only.
// Looks for an environment variable that forces the use of HTTP for all the endpoints. We
// are doing this for ease of running the Playwright tests in CI.
static bool ShouldUseHttpForEndpoints()
{
    const string EnvVarName = "GOLDENDELICIOUS_USE_HTTP_ENDPOINTS";
    var envValue = Environment.GetEnvironmentVariable(EnvVarName);

    // Attempt to parse the environment variable value; return true if it's exactly "1".
    return int.TryParse(envValue, out int result) && result == 1;
}
