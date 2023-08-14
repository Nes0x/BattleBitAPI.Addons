using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerChangedTeamArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
    public Team Team { get; internal init; }
}