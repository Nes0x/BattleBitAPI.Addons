using BattleBitAPI.Addons.CommandHandler.Common;

namespace BattleBitAPI.Addons.Examples.Commands;

public class WarnCommand : Command<Player>
{
    [Command(Name = "warn")]
    public Task HandleAsync(string reason, int days)
    {
        Console.WriteLine($"{reason} : {days}");
        return Task.CompletedTask;
    }
}