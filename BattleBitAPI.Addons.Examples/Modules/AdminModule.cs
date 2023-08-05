using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.Examples.Modules.Checkers;

namespace BattleBitAPI.Addons.Examples.Modules;

[AdminChecker]
public class AdminModule : Command<Player>
{
    [Command(Name = "warn")]
    public Task HandleWarnAsync(Player player, string reason)
    {
        Console.WriteLine($"{reason} {Context.ChatChannel}");
        return Task.CompletedTask;
    }

    [Command(Name = "ban")]
    public Task HandleBanAsync(Player target, string reason)
    {
        Console.WriteLine($"{reason} {target.Name}");
        return Task.CompletedTask;
    }
}