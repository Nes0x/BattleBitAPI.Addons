using System.Reflection;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public class Command
{
    public string CommandName { get; init; }
    public string CommandDescription { get; init; }
    public MethodInfo MethodInfo { get; init; }
    public ParameterInfo[] Parameters { get; init; }
    public int RemoveParameters { get; init; }
}