using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

internal class Program {
    private static async Task Main (string[] args) {
        Console.WriteLine ("Hello, World!");
        var environment = Environment.GetEnvironmentVariable ("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder ()
            .SetBasePath (Directory.GetCurrentDirectory ())
            .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
            .AddJsonFile ($"appsettings.{environment}.json", true, true)
            .AddEnvironmentVariables ()
            .Build ();

        string botToken = configuration.GetSection ("DiscordBotSettings") ["BotToken"] ?? "";
        Console.WriteLine ($"Using botToken: '{botToken}'");

        var client = new DiscordSocketClient ();
        await client.LoginAsync (TokenType.Bot, botToken);
        await client.StartAsync ();

        client.UserUpdated += async (oldUser, newUser) => {
            // Create a Channel object by searching for a channel named '#logs' on the server the ban occurred in.
            var groupChannels = await client.GetGroupChannelsAsync ();
            var logChannel = groupChannels.Where (x => x.Name == "logs").FirstOrDefault ();

            if (
                oldUser.Activities?.FirstOrDefault ()?.Name != newUser.Activities?.FirstOrDefault ()?.Name &&
                newUser.Activities?.FirstOrDefault ()?.Type == ActivityType.Playing) {
                var gameName = newUser.Activities.FirstOrDefault ()?.Name;
                var usersPlaying = client.Guilds.SelectMany (g => g.Users)
                    .Where (u => u.Activities?.FirstOrDefault ()?.Name == gameName)
                    .Select (u => u.Username);

                Console.WriteLine ($"{string.Join(", ", usersPlaying)} are playing {gameName}");
            }
        };

        await Task.Delay (-1);
    }

}