using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Addons.EventHandler.Events;

namespace BattleBitAPI.Addons.Examples.Modules.EventModules;

public class OnPlayerModule : EventModule
{
    [Event(EventType = EventType.OnPlayerConnected)]
    public Task HandleOnPlayerDowned(OnPlayerConnectedArgs args)
    {
        return Task.CompletedTask;
    }
}