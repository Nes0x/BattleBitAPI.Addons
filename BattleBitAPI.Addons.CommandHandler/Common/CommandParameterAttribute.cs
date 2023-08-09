namespace BattleBitAPI.Addons.CommandHandler.Common;

[AttributeUsage(AttributeTargets.Parameter)]
public class CommandParameterAttribute : Attribute
{
    public required string Name { get; init; }
}