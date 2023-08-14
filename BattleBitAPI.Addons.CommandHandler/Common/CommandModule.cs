namespace BattleBitAPI.Addons.CommandHandler.Common;

public abstract class CommandModule<TPlayer> where TPlayer : Player
{
    public Context<TPlayer> Context { get; internal set; }
    internal List<Command> Commands { get; set; }
}