namespace BattleBitAPI.Addons.CommandHandler.Common;

[AttributeUsage(AttributeTargets.Method)]
public class CommandAttribute : Attribute
{
    public string Name;
}