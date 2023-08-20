using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerSpawningEvent : EventGameServer
{
    public OnPlayerSpawningEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task<OnPlayerSpawnArguments?> OnPlayerSpawning(AddonPlayer player, OnPlayerSpawnArguments request)
    {
        return (Task<OnPlayerSpawnArguments?>)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerSpawningArgs
            {
                Player = player,
                GameServer = this,
                PlayerSpawnArguments = request
            }
        });
    }


}

public class OnPlayerSpawningArgs : IPlayerArgs, IGameServerArgs
{
    public required OnPlayerSpawnArguments PlayerSpawnArguments { get; init; }
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}