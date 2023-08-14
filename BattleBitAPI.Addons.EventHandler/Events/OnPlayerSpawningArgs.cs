using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerSpawningArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
    public PlayerSpawnRequest PlayerSpawnRequest { get; internal init; }
}