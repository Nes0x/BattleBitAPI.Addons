using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnSavePlayerStatsEvent : EventGameServer
{
    public OnSavePlayerStatsEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnSavePlayerStats(ulong steamId, PlayerStats stats)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnSavePlayerStatsArgs
            {
                SteamId = steamId,
                PlayerStats = stats,
                GameServer = this
            }
        });
    }
}

public class OnSavePlayerStatsArgs : IGameServerArgs
{
    public required ulong SteamId { get; init; }
    public required PlayerStats PlayerStats { get; init; }
    public required AddonGameServer GameServer { get; init; }
}