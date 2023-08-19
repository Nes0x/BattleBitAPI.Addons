using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerGivenUpEvent : EventGameServer
{
    public OnPlayerGivenUpEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerGivenUp(AddonPlayer player)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerGivenUpArgs
            {
                Player = player,
                GameServer = this
            }
        });
    }
}

public class OnPlayerGivenUpArgs : IPlayerArgs, IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}