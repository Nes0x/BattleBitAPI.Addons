using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerRequestingToChangeRoleEvent : EventGameServer
{
    public OnPlayerRequestingToChangeRoleEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task<bool> OnPlayerRequestingToChangeRole(AddonPlayer player, GameRole requestedRole)
    {
        return (Task<bool>)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerRequestingToChangeRoleArgs()
            {
                Player = player,
                GameServer = this,
                GameRole = requestedRole
            }
        
        });
    }
}

public class OnPlayerRequestingToChangeRoleArgs : IPlayerArgs, IGameServerArgs
{
    public required AddonPlayer Player { get; init; }
    public required AddonGameServer GameServer { get; init; }
    public required GameRole GameRole { get; init; }
}