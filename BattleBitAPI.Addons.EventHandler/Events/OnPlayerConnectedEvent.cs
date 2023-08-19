using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerConnectedEvent : EventGameServer
{
    public OnPlayerConnectedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerConnected(AddonPlayer player)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerConnectedArgs
            {
                Player = player,
                GameServer = this
            }
        });
    }
}

public class OnPlayerConnectedArgs : IPlayerArgs, IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}