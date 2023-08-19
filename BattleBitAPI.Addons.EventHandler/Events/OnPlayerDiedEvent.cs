using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerDiedEvent : EventGameServer
{
    public OnPlayerDiedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerDied(AddonPlayer player)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerDiedArgs
            {
                Player = player,
                GameServer = this
            }
        });
    }
}

public class OnPlayerDiedArgs : IPlayerArgs, IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}