using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerSpawnedEvent : EventGameServer
{
    public OnPlayerSpawnedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerSpawned(AddonPlayer player)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerSpawnedArgs
            {
                Player = player,
                GameServer = this
            }
        });
    }
}

public class OnPlayerSpawnedArgs : IPlayerArgs, IGameServerArgs
{
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}