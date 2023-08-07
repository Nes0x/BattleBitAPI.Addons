using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.Examples.Modules.Checkers;
using BattleBitAPI.Addons.Examples.Services;

namespace BattleBitAPI.Addons.Examples.Modules;

[AdminChecker]
public class AdminModule : Command<Player>
{
    private readonly ConfigService _config;

    public AdminModule(ConfigService config)
    {
        _config = config;
    }

    [Command(Name = "warn")]
    public Task HandleWarnAsync(Player player, string reason)
    {
        Console.WriteLine($"{reason} {Context.ChatChannel} {_config.AdminId}");
        return Task.CompletedTask;
    }

    [Command(Name = "ban")]
    public Task HandleBanAsync(Player target, string reason)
    {
        Console.WriteLine($"{reason} {target.Name}");
        return Task.CompletedTask;
    }
}