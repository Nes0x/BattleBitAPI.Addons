using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnRoundStartedEvent : EventGameServer
{
    public OnRoundStartedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnRoundStarted()
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnRoundStartedArgs
            {
                GameServer = this
            }
        });
    }
}

public class OnRoundStartedArgs : IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
}