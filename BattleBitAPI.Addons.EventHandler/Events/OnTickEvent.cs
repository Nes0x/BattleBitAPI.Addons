using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnTickEvent : EventGameServer
{
    public OnTickEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnTick()
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnTickArgs()
            {
                GameServer = this
            }
        
        });
    }
}

public class OnTickArgs : IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
}