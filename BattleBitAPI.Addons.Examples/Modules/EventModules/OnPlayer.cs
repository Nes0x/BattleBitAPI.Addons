using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Addons.EventHandler.Events;

namespace BattleBitAPI.Addons.Examples.Modules.EventModules;

public class OnPlayer : EventModule<Player>
{
    [Event(EventType = EventType.OnPlayerConnected)]
    public Task HandleOnPlayerConnected(OnPlayerConnectedArgs<Player> args)
    {
        return Task.CompletedTask;
    }
}