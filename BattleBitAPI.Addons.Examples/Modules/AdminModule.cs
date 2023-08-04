using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.Examples.Modules.Checkers;

namespace BattleBitAPI.Addons.Examples.Modules;

public class AdminModule : Command<Player>
{
    [Command(Name = "warn")]
    [AdminChecker]
    public Task HandleWarnAsync(Player target, string reason)
    {
        Console.WriteLine($"{reason} {Context.ChatChannel}");
        return Task.CompletedTask;
    }

    [Command(Name = "ban")]
    [AdminChecker]
    public Task HandleBanAsync(Player target, string reason)
    {
        Console.WriteLine($"{reason} {target.Name}");
        return Task.CompletedTask;
    }
}