using System.Reflection;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public class MethodRepresentation
{
    internal string CommandName { get; init; }
    internal MethodInfo MethodInfo { get; init; }
}