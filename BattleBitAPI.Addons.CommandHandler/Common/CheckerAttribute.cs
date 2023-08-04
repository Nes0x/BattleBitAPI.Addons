namespace BattleBitAPI.Addons.CommandHandler.Common;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public abstract class CheckerAttribute<TPlayer> : Attribute where TPlayer : Player
{
    public Context<TPlayer> Context { get; internal set; }
    public abstract bool RunCommand();
}