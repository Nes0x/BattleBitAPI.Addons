namespace BattleBitAPI.Addons.CommandHandler.Common;

public abstract class Command<TPlayer> where TPlayer : Player
{
    public Context<TPlayer> Context { get; internal set; }
    internal List<MethodRepresentation> MethodHandlers { get; set; }
}