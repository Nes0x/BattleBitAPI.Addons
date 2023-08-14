using System.Reflection;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public class Command
{
    internal string CommandName { get; init; }
    internal string CommandDescription { get; init; }
    internal MethodInfo MethodInfo { get; init; }
    internal ParameterInfo[] Parameters { get; init; }
    internal int RemoveParameters { get; init; }
}