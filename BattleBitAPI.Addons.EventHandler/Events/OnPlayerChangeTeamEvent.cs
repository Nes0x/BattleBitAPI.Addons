using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerChangeTeamEvent : EventGameServer
{
    public OnPlayerChangeTeamEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerChangeTeam(AddonPlayer player, Team team)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerChangeTeamArgs()
            {
                Player = player,
                Team = team
            }
        
        });
    }
}

public class OnPlayerChangeTeamArgs : IPlayerArgs
{
    public required AddonPlayer Player { get; init; }
    public required Team Team { get; init; }
}