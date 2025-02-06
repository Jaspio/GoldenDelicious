using System.Text.Json;
using GoldenDelicious.DiscordBot.Data.Contexts;

namespace GoldenDelicious.DiscordBot.API.Data;

public partial class DiscordBotContextSeed(
    IWebHostEnvironment env,
    IOptions<DiscordBotApiOptions> settings,
    ILogger<DiscordBotContextSeed> logger) : IDbSeeder<DiscordBotContext>
{
    public async Task SeedAsync(DiscordBotContext context)
    {
        var contentRootPath = env.ContentRootPath;
        var picturePath = env.WebRootPath;
        
        await context.Database.OpenConnectionAsync();

        if (!context.Servers.Any())
        {
            var useCustomizationData = settings.Value.LogVerbose;
            var sourcePath = Path.Combine(contentRootPath, "Setup", "catalog.json");
            var sourceJson = await File.ReadAllTextAsync(sourcePath);
            var sourceItems = JsonSerializer.Deserialize<CatalogSourceEntry[]>(sourceJson);
            
            logger.LogInformation("Seeded catalog with {NumItems} items", context.Servers.Count());
            await context.SaveChangesAsync();
        }
    }
    
    private class CatalogSourceEntry
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public required string Brand { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
    }
}