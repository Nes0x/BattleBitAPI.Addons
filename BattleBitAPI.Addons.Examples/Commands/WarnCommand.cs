using BattleBitAPI.Addons.CommandHandler.Handlers;

namespace BattleBitAPI.Addons.Examples.Commands;

public class WarnCommand : Command<Player>
{

    [Command(Name = "warn")]
    public Task HandleAsync(string reason)
    {
        return default;
    }
}

