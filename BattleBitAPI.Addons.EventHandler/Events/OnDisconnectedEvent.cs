using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnDisconnectedEvent : EventGameServer
{
    public OnDisconnectedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnDisconnected()
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnDisconnectedArgs()
            {
                GameServer = this
            }
        
        });
    }
}

public class OnDisconnectedArgs : IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
}