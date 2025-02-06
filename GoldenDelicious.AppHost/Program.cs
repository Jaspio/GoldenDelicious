using GoldenDelicious.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();

// Add Aspire hosting components.
var redis = builder.AddRedis("redis");
var rabbitMq = builder.AddRabbitMQ("eventbus");
var postgres = builder.AddPostgres("postgres").WithImage("ankane/pgvector").WithImageTag("latest");

var discordBotDb = postgres.AddDatabase("discordbotdb");

var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "https";

// Add services to the container.
var botApi = builder
    .AddProject<Projects.DiscordBot_API>("bot-api")
    .WithReference(rabbitMq)
    .WithReference(discordBotDb);

var botService = builder
    .AddProject<Projects.DiscordBot_Service>("bot-service")
    .WithReference(discordBotDb);

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
