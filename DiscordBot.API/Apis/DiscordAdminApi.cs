using GoldenDelicious.DiscordBot.API.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GoldenDelicious.DiscordBot.API.Apis;

public static class DiscordAdminApi
{
    public static IEndpointRouteBuilder MapDiscordAdminApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/discord").HasApiVersion(1.0);
        
        // Routes for querying discord server data.
        // todo
        
        // Routes for managing discord server.
        api.MapPut("/server/{serverId}/kick/{userId}", KickUserFromServer);
        
        app.MapGet("/admin", async context =>
        {
            await context.Response.WriteAsJsonAsync(new { Message = "Hello, Discord Admin!" });
        });

        return app;
    }
    
    public static async Task<Results<NoContent, NotFound<string>>> KickUserFromServer(
        [AsParameters] DiscordBotApiServices services,
        [FromRoute] string serverId,
        [FromRoute] string userId)
    {
        var catalogItem = await services.Context.Servers.SingleOrDefaultAsync(i => i.Id.ToString() == userId);
        
        if (catalogItem == null)
        {
            return TypedResults.NotFound($"User with id {userId} not found on server {serverId}.");
        }
        
        // todo
        return TypedResults.NoContent();
    }
}