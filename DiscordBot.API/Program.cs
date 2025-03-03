using GoldenDelicious.DiscordBot.API.Extensions;
using GoldenDelicious.DiscordBot.Data.Extensions;
using GoldenDelicious.DiscordBot.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

        
// GoldenDelicious.ServiceDefaults
builder.AddBasicServiceDefaults();

// GoldenDelicious.DiscordBot.API.Extensions.ServiceCollectionExtensions
builder.AddApiServices();

// GoldenDelicious.DiscordBot.Data.Extensions.ServiceCollectionExtensions
builder.AddPostgresDatabaseServices();

// GoldenDelicious.DiscordBot.Service.Extensions.ServiceCollectionExtensions
builder.AddApplicationServices();


builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);

var app = builder.Build();

app.MapDefaultEndpoints();

app.NewVersionedApi("DiscordBot");

app.UseDefaultOpenApi();
await app.RunAsync();
