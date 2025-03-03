using GoldenDelicious.Common.Data;
using GoldenDelicious.DiscordBot.Data.Contexts;
using GoldenDelicious.DiscordBot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldenDelicious.DiscordBot.Data.Repositories;

public class ServerRepository(DiscordBotDbContext dbContext) : IServerRepository
{
    public async Task<DiscordServer?> GetServer(ulong guildId)
    {
        var post = await dbContext.Servers
            .Where(p => p.GuildId == guildId)
            .Select(p => (DiscordServer)p)
            .FirstOrDefaultAsync();

        return post;
    }
    
    public async Task<IEnumerable<DiscordServer>> GetAllServers(int count, int page)
    {
        var posts = await dbContext.Servers
            .OrderByDescending(s => s.GuildId)
            .Skip(count * (page - 1))
            .Take(count)
            .ToListAsync();

        return posts.Select(entity => (DiscordServer)entity);
    }
    
    public async Task AddServerAsync(DiscordServer server)
    {
        var entity = (ServerEntity)server;
        
        dbContext.Servers.Add(entity);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task UpdatePostAsync(DiscordServer server)
    {
        var entity = (ServerEntity)server;
        
        dbContext.Servers.Update(entity);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task DeletePostAsync(ulong guildId)
    {
        var entity = await dbContext.Servers.FindAsync(guildId);
        
        if (entity != null)
        {
            dbContext.Servers.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}