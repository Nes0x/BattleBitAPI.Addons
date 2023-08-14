namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerSpawnedArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
}