using GoldenDelicious.DiscordBot.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// GoldenDelicious.ServiceDefaults
builder.AddBasicServiceDefaults();

// GoldenDelicious.DiscordBot.Data.Extensions.ServiceCollectionExtensions
builder.AddPostgresDatabaseServices();

// GoldenDelicious.DiscordBot.Service.Extensions.ServiceCollectionExtensions
builder.AddApplicationServices();

var app = builder.Build();

app.MapDefaultEndpoints();
await app.RunAsync();
