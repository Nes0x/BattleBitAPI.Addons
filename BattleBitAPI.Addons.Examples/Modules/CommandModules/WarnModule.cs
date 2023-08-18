using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.Examples.Services;

namespace BattleBitAPI.Addons.Examples.Modules.CommandModules;

[Command(Name = "warn")]
public class WarnModule : CommandModule
{
    private readonly ConfigService _config;

    public WarnModule(ConfigService config)
    {
        _config = config;
    }

    [Command(Name = "add")]
    public Task HandleAdd(string target, string reason)
    {
        Console.WriteLine($"{reason} {Context.ChatChannel} {_config.AdminId}");
        return Task.CompletedTask;
    }
}