using System.Reflection;

namespace BattleBitAPI.Addons.EventHandler.Common;

public class Event
{
    internal MethodInfo MethodInfo { get; init; }
    internal EventType EventType { get; init; }
}