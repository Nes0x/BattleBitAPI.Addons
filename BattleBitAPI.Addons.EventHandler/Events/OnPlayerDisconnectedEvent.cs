using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerDisconnectedEvent : EventGameServer
{
    public OnPlayerDisconnectedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerDisconnected(AddonPlayer player)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerDisconnectedArgs
            {
                Player = player,
                GameServer = this
            }
        });
    }
}

public class OnPlayerDisconnectedArgs : IPlayerArgs, IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}