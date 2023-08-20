using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerLeftSquadEvent : EventGameServer
{
    public OnPlayerLeftSquadEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerLeftSquad(AddonPlayer player, Squad<AddonPlayer> squad)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerLeftSquadArgs
            {
                Player = player,
                GameServer = this,
                Squad = squad
            }
        });
    }
}

public class OnPlayerLeftSquadArgs : IPlayerArgs, IGameServerArgs
{
    public required Squad<AddonPlayer> Squad { get; init; }
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}