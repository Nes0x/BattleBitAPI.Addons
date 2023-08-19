using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerJoinedSquadEvent : EventGameServer
{
    public OnPlayerJoinedSquadEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerJoinedSquad(AddonPlayer player, Squads squad)
    {
        {
            return (Task)Event.MethodInfo.Invoke(EventModule, new[]
            {
                new OnPlayerJoinedSquadArgs
                {
                    Player = player,
                    GameServer = this,
                    Squad = squad
                }
            });
        }
    }
}

public class OnPlayerJoinedSquadArgs : IPlayerArgs, IGameServerArgs
{
    public required Squads Squad { get; init; }
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}