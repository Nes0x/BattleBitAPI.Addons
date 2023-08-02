namespace BattleBitAPI.Addons.CommandHandler.Common;

[AttributeUsage(AttributeTargets.Method)]
public abstract class CheckerAttribute<TPlayer> : Attribute where TPlayer : Player
{
    public Context<TPlayer> Context { get; internal set; }
    public abstract bool RunCommand();
}