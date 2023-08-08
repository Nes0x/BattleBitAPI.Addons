namespace BattleBitAPI.Addons.CommandHandler.Common;

public abstract class CommandModule<TPlayer> where TPlayer : Player
{
    public Context<TPlayer> Context { get; internal set; }
    public List<Command> Commands { internal get; set; }
}