namespace BattleBitAPI.Addons.CommandHandler.Common;

public abstract class CommandModule
{
    public Context Context { get; internal set; }
    internal List<Command> Commands { get; set; }
}