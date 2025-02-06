using GoldenDelicious.DiscordBot.API.Apis;
using GoldenDelicious.DiscordBot.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);

var app = builder.Build();

app.MapDefaultEndpoints();

app.NewVersionedApi("DiscordBot").MapDiscordAdminApi();

app.UseDefaultOpenApi();
await app.RunAsync();
