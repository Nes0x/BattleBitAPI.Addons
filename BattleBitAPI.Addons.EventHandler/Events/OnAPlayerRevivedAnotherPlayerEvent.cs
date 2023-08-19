using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnAPlayerRevivedAnotherPlayerEvent : EventGameServer
{
    public OnAPlayerRevivedAnotherPlayerEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnAPlayerRevivedAnotherPlayer(AddonPlayer from, AddonPlayer to)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnAPlayerRevivedAnotherPlayerArgs
            {
                From = from,
                To = to,
                GameServer = this
            }
        });
    }
}

public class OnAPlayerRevivedAnotherPlayerArgs : IGameServerArgs
{
    public required AddonPlayer From { get; init; }
    public required AddonPlayer To { get; init; }
    public required AddonGameServer GameServer { get; init; }
}