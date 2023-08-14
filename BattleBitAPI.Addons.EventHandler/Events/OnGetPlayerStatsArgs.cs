using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnGetPlayerStatsArgs
{
    public ulong SteamId { get; internal init; }
    public PlayerStats PlayerStats { get; internal init; }
}