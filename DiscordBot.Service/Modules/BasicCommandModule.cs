using Discord.Commands;

namespace GoldenDelicious.DiscordBot.Service.Modules;

public class BasicCommandModule : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    public async Task PingAsync()
    {
        await ReplyAsync("pong");
    }
}
