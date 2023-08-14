namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerDisconnectedArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
}