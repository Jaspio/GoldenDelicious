using GoldenDelicious.DiscordBot.Data.Contexts;
using GoldenDelicious.ServiceDefaults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<DiscordBotDbContext>(
    "discordbotdb",
    configureDbContextOptions: dbContextOptionsBuilder =>
    {
        dbContextOptionsBuilder.UseNpgsql(contextOptionsBuilder =>
        {
            contextOptionsBuilder.UseVector();
        });
    }
);

if (app.Environment.IsDevelopment())
{
    // Retrieve an instance of the DbContext class and manually run migrations during startup
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DiscordBotDbContext>();
    await context.Database.MigrateAsync();
}

builder.Services.AddOpenTelemetry();

app.MapDefaultEndpoints();

await app.RunAsync();
