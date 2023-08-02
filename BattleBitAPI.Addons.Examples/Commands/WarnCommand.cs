using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.Examples.Commands.Checkers;

namespace BattleBitAPI.Addons.Examples.Commands;

public class WarnCommand : Command<Player>
{
    [Command(Name = "warn")]
    [AdminChecker]
    public Task HandleAsync(string reason, int days = 8)
    {
        Console.WriteLine($"{reason} : {days}");
        return Task.CompletedTask;
    }
}