using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerRequestingToChangeTeamEvent : EventGameServer
{
    public OnPlayerRequestingToChangeTeamEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task<bool> OnPlayerRequestingToChangeTeam(AddonPlayer player, Team requestedTeam)
    {
        return (Task<bool>)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerRequestingToChangeTeamArgs()
            {
                Player = player,
                GameServer = this,
                Team = requestedTeam
            }
        
        });
    }


}

public class OnPlayerRequestingToChangeTeamArgs : IPlayerArgs, IGameServerArgs
{
    public required AddonPlayer Player { get; init; }
    public required AddonGameServer GameServer { get; init; }
    public required Team Team { get; init; }
}