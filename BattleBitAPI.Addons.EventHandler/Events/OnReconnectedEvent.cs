using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnReconnectedEvent : EventGameServer
{
    public OnReconnectedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnReconnected()
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnReconnectedArgs()
            {
                GameServer = this
            }
        
        });
    }
}

public class OnReconnectedArgs : IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
}