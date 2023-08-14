namespace BattleBitAPI.Addons.CommandHandler.Common;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class CommandAttribute : Attribute
{
    public string Description { get; init; } = "";
    public required string Name { get; init; }
}