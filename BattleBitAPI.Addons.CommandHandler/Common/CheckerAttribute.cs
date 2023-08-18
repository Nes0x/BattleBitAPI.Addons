namespace BattleBitAPI.Addons.CommandHandler.Common;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public abstract class CheckerAttribute : Attribute
{
    public Context Context { get; internal set; }
    public abstract bool RunCommand();
}