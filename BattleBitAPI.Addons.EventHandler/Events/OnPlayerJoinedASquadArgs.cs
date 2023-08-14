using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerJoinedASquadArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
    public Squads Squads { get; internal init; }
}