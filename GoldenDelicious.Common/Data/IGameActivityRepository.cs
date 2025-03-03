namespace GoldenDelicious.Common.Data;

public interface IGameActivityRepository
{
    Task<GameActivity> GetGameActivityAsync(ulong id);
    Task<IEnumerable<GameActivity>> GetAllGameActivitiesAsync(int count, int page);
    Task<IEnumerable<GameActivity>> GetUserGameActivitiesAsync(ulong userId);
    Task<IEnumerable<GameActivity>> GetServerGameActivitiesAsync(ulong serverId);
    
    Task AddGameActivityAsync(GameActivity activity);
}