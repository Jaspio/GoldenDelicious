var builder = WebApplication.CreateBuilder(args);

// GoldenDelicious.ServiceDefaults
builder.AddBasicServiceDefaults();

// GoldenDelicious.DiscordBot.Extensions.ServiceCollectionExtensions
builder.AddApplicationServices();

var app = builder.Build();

app.MapDefaultEndpoints();
await app.RunAsync();
