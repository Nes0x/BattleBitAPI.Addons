using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerChangedRoleEvent : EventGameServer
{
    public OnPlayerChangedRoleEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerChangedRole(AddonPlayer player, GameRole role)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerChangedRoleArgs
            {
                Player = player,
                GameServer = this,
                GameRole = role
            }
        });
    }
}

public class OnPlayerChangedRoleArgs : IPlayerArgs, IGameServerArgs
{
    public required GameRole GameRole { get; init; }
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}