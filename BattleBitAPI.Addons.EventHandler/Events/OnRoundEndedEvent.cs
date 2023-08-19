using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnRoundEndedEvent : EventGameServer
{
    public OnRoundEndedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnRoundEnded()
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnRoundEndedArgs
            {
                GameServer = this
            }
        });
    }
}

public class OnRoundEndedArgs : IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
}