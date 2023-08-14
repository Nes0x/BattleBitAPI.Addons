namespace BattleBitAPI.Addons.EventHandler.Common;

public abstract class EventModule<TPlayer> where TPlayer : Player
{
    internal List<Event> Events { get; set; }
}