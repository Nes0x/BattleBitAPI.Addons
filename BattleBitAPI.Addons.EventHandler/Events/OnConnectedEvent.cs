using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnConnectedEvent : EventGameServer
{
    public OnConnectedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnConnected()
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnConnectedArgs
            {
                GameServer = this
            }
        });
    }
}

public class OnConnectedArgs : IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
}