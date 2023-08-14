namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerDiedArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
}