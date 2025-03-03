namespace GoldenDelicious.Common.Data;

public interface IServerRepository
{
    Task AddServerAsync(DiscordServer server);
    Task UpdatePostAsync(DiscordServer server);
    Task DeletePostAsync(ulong guildId);
    Task<DiscordServer?> GetServer(ulong guildId);
    Task<IEnumerable<DiscordServer>> GetAllServers(int count, int page);
}