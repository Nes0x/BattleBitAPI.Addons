namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerConnectedArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
}