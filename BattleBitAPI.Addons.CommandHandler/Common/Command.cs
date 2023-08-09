using System.Reflection;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public class Command
{
    public required string CommandName { get; init; }
    public required string CommandDescription { get; init; }
    public required MethodInfo MethodInfo { get; init; }
    public required ParameterInfo[] Parameters { get; init; }
    public required int RemoveParameters { get; init; }
}