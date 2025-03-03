using GoldenDelicious.Common.Data;
using GoldenDelicious.DiscordBot.Data.Contexts;
using GoldenDelicious.DiscordBot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldenDelicious.DiscordBot.Data.Repositories;

public class GameActivityRepository(DiscordBotDbContext dbContext) : IGameActivityRepository
{
    public async Task AddGameActivityAsync(GameActivity activity)
    {
        var gameActivity = (GameActivityEntity)activity;
        dbContext.GameActivities.Add(gameActivity);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<GameActivity?> GetGameActivityById(ulong id)
    {
        var activity = await dbContext.GameActivities
            .Where(p => p.Id == id)
            .Select(p => (GameActivity)p)
            .FirstOrDefaultAsync();

        return activity;
    }

    public Task<GameActivity> GetGameActivityAsync(ulong id)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<GameActivity>> GetAllGameActivitiesAsync(int count,
        int page)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<GameActivity>> GetUserGameActivitiesAsync(ulong userId)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<GameActivity>> GetServerGameActivitiesAsync(ulong serverId)
    {
        throw new NotImplementedException();
    }
}