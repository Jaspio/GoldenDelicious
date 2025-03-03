using Discord;
using Discord.WebSocket;
using GoldenDelicious.DiscordBot.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GoldenDelicious.DiscordBot.API.Apis;

public abstract class DiscordAdminApi()
{
    public IEndpointRouteBuilder MapDiscordAdminApi(IEndpointRouteBuilder app)
    {
        var discordBotService = app.ServiceProvider.GetRequiredService<DiscordBotService>();
        
        var api = app.MapGroup("api/discord").HasApiVersion(1.0);

        // Routes for querying discord server data.
        // todo

        // Routes for managing discord server.
        api.MapPut("/server/{serverId}/kick/{userId}", async (ulong serverId, ulong userId) =>
        {
            var result = await KickUserFromServer(discordBotService, serverId, userId);
            return result;
        });

        app.MapGet("/admin", async context =>
        {
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Hello, Discord Admin!"
            });
        });

        return app;
    }

    public static async Task<Results<NoContent, NotFound<string>>> KickUserFromServer(
        DiscordBotService discordBotService,
        [FromRoute] ulong serverId,
        [FromRoute] ulong userId)
    {
        var user = await discordBotService.DiscordClient.GetUserAsync(userId);
        SocketGuild guild = discordBotService.DiscordClient.GetGuild(serverId);

        bool serverHasUser = guild?.Users?.Contains(user) ?? false;
        
        if (user == null || !serverHasUser)
        {
            return TypedResults.NotFound($"User with id {userId} not found on server {serverId}.");
        }

        if (guild != null)
        {
            await guild.BanUserAsync(user, 0, new RequestOptions()
            {
                AuditLogReason = "Kicked via API"
            });
        }

        return TypedResults.NoContent();
    }
}