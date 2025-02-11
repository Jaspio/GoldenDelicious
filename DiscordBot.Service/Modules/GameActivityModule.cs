using System.Data;
using System.Text;
using Discord;
using Discord.Commands;

namespace DiscordBot.Service.Modules;

public class GameActivityModule : ModuleBase<SocketCommandContext>
{
    [Command("playing")]
    [Summary("Shows current gaming activities of all guild members")]
    public async Task ShowGamingActivitiesAsync()
    {
        var guild = Context.Guild;
        var activities = new Dictionary<string, List<string>>();

        foreach (var user in guild.Users)
        {
            var gameActivity = user.Activities!.FirstOrDefault(a => a.Type == ActivityType.Playing);

            if (gameActivity != null)
            {
                if (activities.ContainsKey(gameActivity.Name))
                {
                    activities[gameActivity.Name] = [];
                }
                activities[gameActivity.Name].Add(user.Username);

                // Here you would log to your database
                // await LogActivityToDatabase(user.Id, gameActivity.Name, DateTime.UtcNow);
            }
        }

        var response = FormatActivityResponse(activities);
        await ReplyAsync(response);
    }

    private string FormatActivityResponse(Dictionary<string, List<string>> activities)
    {
        if (activities.Count == 0)
        {
            return "No one is currently playing any games.";
        }

        var sb = new StringBuilder();
        sb.AppendLine("```");
        sb.AppendLine("Current Gaming Activities:");
        sb.AppendLine("------------------------");

        foreach (var activity in activities.OrderBy(a => a.Key))
        {
            sb.AppendLine($"{activity.Key}:");
            foreach (var player in activity.Value.OrderBy(p => p))
            {
                sb.AppendLine($"  - {player}");
            }
            sb.AppendLine();
        }

        sb.AppendLine("```");
        return sb.ToString();
    }

    // Example database logging method
    /*
    private async Task LogActivityToDatabase(ulong userId, string gameName, DateTime timestamp)
    {
        var activity = new GameActivity
        {
            UserId = userId,
            GameName = gameName,
            Timestamp = timestamp
        };

        _dbContext.GameActivities.Add(activity);
        await _dbContext.SaveChangesAsync();
    }
    */
}
