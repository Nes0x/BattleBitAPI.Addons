using System.Reflection;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public abstract class Command<TPlayer> where TPlayer : Player
{
    public Context<TPlayer> Context { get; internal set; }
    internal string CommandName { get; set; }
    internal MethodInfo MethodInfo { get; set; }

}